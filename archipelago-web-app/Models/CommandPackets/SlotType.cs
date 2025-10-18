using System;

namespace archipelago_web_app.Models.CommandPackets;

public enum SlotType
{
    spectator = 0b000,
    player = 0b001,
    group = 0b010
}
