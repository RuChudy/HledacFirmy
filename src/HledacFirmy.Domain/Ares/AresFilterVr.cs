using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HledacFirmy.Ares;

public sealed class AresFilterVr
{
    [JsonPropertyName("start")]
    public int? Start { get; set; }

    [JsonPropertyName("pocet")]
    public int? Pocet { get; set; }

    [JsonPropertyName("razeni")]
    public List<string>? Razeni { get; set; } = new List<string>();

    [JsonPropertyName("ico")]
    public List<string>? Ico { get; set; }
}
