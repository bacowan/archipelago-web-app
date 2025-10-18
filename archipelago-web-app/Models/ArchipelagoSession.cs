using System;
using System.Net.WebSockets;
using archipelago_web_app.Events;

namespace archipelago_web_app.Models;

public class ArchipelagoSession
{
    private ClientWebSocket? activeWebSocket;

    public bool IsConnectionOpen => activeWebSocket?.State == WebSocketState.Open;

    public event EventHandler<ConnectionStateChangedEventArgs>? ConnectionStateChanged;

    public async Task NewSession(string connectionString)
    {
        // in case we already have a connection open, close it
        if (activeWebSocket != null && activeWebSocket.State == WebSocketState.Open)
        {
            await activeWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client Closed", default);
            ConnectionStateChanged?.Invoke(this, new(false));
        }

        // create the new connection
        activeWebSocket = new ClientWebSocket();
        await activeWebSocket.ConnectAsync(new("wss://" + connectionString), default);
        ConnectionStateChanged?.Invoke(this, new(true));
    }
}
