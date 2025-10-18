using System;

namespace archipelago_web_app.Events;

public readonly record struct ConnectionStateChangedEventArgs(bool IsConnected);