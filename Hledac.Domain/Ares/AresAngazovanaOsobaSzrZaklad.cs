namespace Hledac.Domain.Ares;

/// <summary>
/// Osoba v angažmá ekonomického subjektu
/// </summary>
public sealed class AresAngazovanaOsobaSzrZaklad
{
    /// <summary>Jméno fyzické osoby</summary>
    public string? jmeno { get; set; }

    /// <summary>Příjmení fyzické osoby</summary>
    public string? prijmeni { get; set; }

    /// <summary>Titul před jménem fyzické osoby</summary>
    public string? titulPredJmenem { get; set; }

    /// <summary>Titul za jménem fyzické osoby</summary>
    public string? titulZaJmenem { get; set; }

    /// <summary>Datum narození fyzické osoby</summary>
    public DateTime? datumNarozeni { get; set; }

    /// <summary>Typ angažmá osoby - kód(ciselnikKod: TypAngazma)</summary>
    public string? typAngazma { get; set; }
}
