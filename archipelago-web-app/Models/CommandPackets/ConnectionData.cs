using System;

namespace archipelago_web_app.Models.CommandPackets;

public readonly record struct ConnectionData(
    int team,
    int slot,
    List<NetworkPlayer> players,
    List<int> missing_locations,
    List<int> checked_locations,
    Dictionary<string, string> slot_data,
    Dictionary<int, NetworkSlot> slot_info,
    int hint_points);