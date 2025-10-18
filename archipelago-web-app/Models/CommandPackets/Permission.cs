using System;

namespace archipelago_web_app.Models.CommandPackets;

public enum Permission
{
    disabled = 0b000,
    enabled = 0b001,
    goal = 0b010,
    auto = 0b110,
    auto_enabled = 0b111
}
