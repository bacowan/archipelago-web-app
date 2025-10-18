using System;

namespace archipelago_web_app.Models.CommandPackets;

public readonly record struct NetworkSlot
(
    string name,
    string game,
    SlotType type,
    List<int> group_members);