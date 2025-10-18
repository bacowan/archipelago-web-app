using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace archipelago_web_app.Models.CommandPackets;

public readonly record struct Version(
    int major,
    int minor,
    int build)
{
    [JsonPropertyName("class")]
    public string _class => "Version";
}