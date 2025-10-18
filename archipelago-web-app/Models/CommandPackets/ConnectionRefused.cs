using System;

namespace archipelago_web_app.Models.CommandPackets;

public readonly record struct ConnectionRefused(List<string> errors);