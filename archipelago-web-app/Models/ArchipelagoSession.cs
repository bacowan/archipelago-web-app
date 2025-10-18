using System;
using System.Net.WebSockets;
using System.Text;
using archipelago_web_app.Events;
using archipelago_web_app.Models.CommandPackets;
using archipelago_web_app.Models.Exceptions;
using System.Text.Json;

namespace archipelago_web_app.Models;

public class ArchipelagoSession
{
    private const string gameName = "Jigsaw";

    private ClientWebSocket? activeWebSocket;

    public bool IsConnectionOpen => activeWebSocket?.State == WebSocketState.Open;

    public event EventHandler<ConnectionStateChangedEventArgs>? ConnectionStateChanged;

    private static async Task<string> receiveMessage(WebSocket webSocket)
    {
        var buffer = new byte[1024];
        var fullMessage = new StringBuilder();

        WebSocketReceiveResult result;
        do
        {
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var chunk = Encoding.UTF8.GetString(buffer, 0, result.Count);
            fullMessage.Append(chunk);
        } while (!result.EndOfMessage);

        return fullMessage.ToString();
    }

    private async Task CloseActiveConnectionIfOpen()
    {
        if (activeWebSocket != null && activeWebSocket.State == WebSocketState.Open)
        {
            await activeWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client Closed", default);
            ConnectionStateChanged?.Invoke(this, new(false));
        }
    }

    public async Task NewSession(string connectionString, string player, string password)
    {
        // in case we already have a connection open, close it
        await CloseActiveConnectionIfOpen();

        activeWebSocket = new ClientWebSocket();

        // Follow the connection handshake steps here:
        // https://github.com/ArchipelagoMW/Archipelago/blob/main/docs/network%20protocol.md

        // 1. Client establishes WebSocket connection to Archipelago server.
        await activeWebSocket.ConnectAsync(new("wss://" + connectionString), default);

        try
        {

            //  2. Server accepts connection and responds with a RoomInfo packet.
            var roomInfoString = await receiveMessage(activeWebSocket);
            var roomInfo = JsonSerializer.Deserialize<List<RoomInfo>>(roomInfoString);

            if (roomInfo == null)
            {
                throw new InvalidResponseException("Failed to parse Room Info");
            }

            // Steps 3 and 4 are skipped

            // 5. Client sends Connect packet in order to authenticate with the server.
            var packetData = new ConnectCommand(
                password,
                gameName,
                player,
                Guid.NewGuid().ToString(),
                roomInfo.First().version,
                ItemsHandling.FromOtherWorlds & ItemsHandling.FromOwnWorld & ItemsHandling.StartingInventory,
                new List<string> { },
                false);

            var packetString = JsonSerializer.Serialize(new List<ConnectCommand> { packetData });
            var packetBuffer = Encoding.UTF8.GetBytes(packetString);

            await activeWebSocket.SendAsync(
                new ArraySegment<byte>(packetBuffer, 0, packetBuffer.Length),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);

            // 6. Server validates the client's packet and responds with Connected or ConnectionRefused.
            var connectionResult = await receiveMessage(activeWebSocket);
            var connectionResultDocument = JsonDocument.Parse(connectionResult);

            if (connectionResultDocument.RootElement[0].TryGetProperty("slot", out _))
            {
                var connectionResultObject = JsonSerializer.Deserialize<List<ConnectedResult>>(connectionResult);
            }
            else
            {
                var errors = JsonSerializer.Deserialize<List<ConnectionRefused>>(connectionResult);
                if (errors != null && errors.Count > 0)
                {
                    throw new ConnectionRefusedException(string.Join("; ", errors[0].errors));
                }
                else
                {
                    throw new ConnectionRefusedException("Failed to connect");
                }
            }
        }
        catch
        {
            // if something goes wrong, simply close the connection and bubble the exception
            await activeWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Failed to connect", default);
            throw;
        }


        ConnectionStateChanged?.Invoke(this, new(true));
    }
}
