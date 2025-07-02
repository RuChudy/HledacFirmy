using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HledacFirmy.Ares;

public sealed class AresFilterRoot
{
    [JsonPropertyName("start")]
    public int? Start { get; set; }

    [JsonPropertyName("pocet")]
    public int? Pocet { get; set; }

    [JsonPropertyName("razeni")]
    public List<string>? Razeni { get; set; } = new List<string>();

    [JsonPropertyName("ico")]
    public List<string>? Ico { get; set; }

    [JsonPropertyName("obchodniJmeno")]
    public string? ObchodniJmeno { get; set; }

    [JsonPropertyName("sidlo")]
    public AresFilterSidlo? Sidlo { get; set; }

    [JsonPropertyName("pravniForma")]
    public List<string>? PravniForma { get; set; }

    [JsonPropertyName("financniUrad")]
    public List<string>? FinancniUrad { get; set; }

    [JsonPropertyName("czNace")]
    public List<string>? CzNace { get; set; }
}

public sealed class AresFilterSidlo
{
    [JsonPropertyName("kodCastiObce")]
    public int? KodCastiObce { get; set; }

    [JsonPropertyName("kodSpravnihoObvodu")]
    public int? KodSpravnihoObvodu { get; set; }

    [JsonPropertyName("kodMestskeCastiObvodu")]
    public int? KodMestskeCastiObvodu { get; set; }

    [JsonPropertyName("kodUlice")]
    public int? KodUlice { get; set; }

    [JsonPropertyName("cisloDomovni")]
    public int? CisloDomovni { get; set; }

    [JsonPropertyName("kodObce")]
    public int? KodObce { get; set; }

    [JsonPropertyName("cisloOrientacni")]
    public int? CisloOrientacni { get; set; }

    [JsonPropertyName("cisloOrientacniPismeno")]
    public string? CisloOrientacniPismeno { get; set; }

    [JsonPropertyName("textovaAdresa")]
    public string? TextovaAdresa { get; set; }
}
