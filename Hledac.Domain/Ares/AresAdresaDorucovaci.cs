namespace Hledac.Domain.Ares;

/// <summary>
/// Adresa doručovací dle vyhlášky 359/2011 sb.
/// </summary>
public sealed class AresAdresaDorucovaci
{
    /// <summary>řádek doručovací adresy</summary>
    public string? radekAdresy1 { get; set; }

    /// <summary>řádek doručovací adresy</summary>
    public string? radekAdresy2 { get; set; }

    /// <summary>řádek doručovací adresy</summary>
    public string? radekAdresy3 { get; set; }
}
