using System;
using Newtonsoft.Json;

namespace archipelago_web_app.Models.CommandPackets;

[Flags]
public enum ItemsHandling : short {
    None = 0,
    FromOtherWorlds = 1,
    FromOwnWorld = 2,
    StartingInventory = 4
};

public readonly record struct ConnectCommand(
    string password,
    string game,
    string name,
    string uuid,
    Version version,
    ItemsHandling items_handling,
    List<string> tags,
    bool slot_data)
{
    public string cmd => "Connect";
};