using System;

namespace archipelago_web_app.Models.CommandPackets;

public readonly record struct RoomInfo(
    Version version,
    Version generator_version,
    List<string> tags,
    bool password,
    Dictionary<string, Permission> permissions,
    int hint_cost,
    int location_check_points,
    List<string> games,
    Dictionary<string, string> datapackage_checksums,
    string seed_name,
    float time);