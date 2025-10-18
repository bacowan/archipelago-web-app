using System;

namespace archipelago_web_app.Models.CommandPackets;

public readonly record struct NetworkPlayer(
    int team,
    int slot,
    string alias,
    string name);