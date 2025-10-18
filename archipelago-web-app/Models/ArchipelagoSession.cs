using System;
using System.Net.WebSockets;

namespace archipelago_web_app.Models;

public class ArchipelagoSession
{
    private ClientWebSocket? activeWebSocket;

    public async Task NewSession(string connectionString)
    {
        // in case we already have a connection open, close it
        if (activeWebSocket != null)
        {
            await activeWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client Closed", default);
        }

        // create the new connection
        activeWebSocket = new ClientWebSocket();
        await activeWebSocket.ConnectAsync(new ("wss://" + connectionString), default);
    }
}
