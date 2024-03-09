namespace Hledac.Domain.Ares;

/// <summary>
/// Základní informace o ekonomickém subjektu
/// </summary>
public sealed class AresEkonomickySubjekt
{
    /// <summary>Identifikační číslo osoby - IČO</summary>
    public string? ico { get; set; }

    /// <summary>Obchodní jméno ekonomického subjektu</summary>
    public string? obchodniJmeno { get; set; }

    public AresAdresa? sidlo { get; set; }

    /// <summary>Právní forma - kód(ciselnikKod: PravniForma, zdroj: res, com)</summary>
    public string? pravniForma { get; set; }

    /// <summary>Správně příslušný finanční úřad - kód(ciselnikKod: FinancniUrad, zdroj:ufo)</summary>
    public string? financniUrad { get; set; }

    /// <summary>Datum vzniku ekonomického subjektu</summary>
    public DateTime? datumVzniku { get; set; }

    /// <summary>Datum zániku ekonomického subjektu</summary>
    public DateTime? datumZaniku { get; set; }

    /// <summary>Datum aktualizace záznamu</summary>
    public DateTime? datumAktualizace { get; set; }

    /// <summary>Daňové identifikační číslo ve formátu CZNNNNNNNNNN</summary>
    public string? dic { get; set; }

    /// <summary>Ičo ekonomického subjektu, pokud je ičo přidělené. Id ekonomického subjektu, pokud je ičo nepřidělené.</summary>
    public string? icoId { get; set; }

    public AresAdresaDorucovaci? adresaDorucovaci { get; set; }

    public AresSeznamRegistraci? seznamRegistraci {get; set; }

    /// <summary>Identifikace primárního zdroje dat.</summary>
    public string? primarniZdroj { get; set; }

    /* TODO: dalsiUdaje	[...] */

    /// <summary>CZ-NACE ekonomického subjektu – kód (ciselnikKod: CzNace, zdroj:res)</summary>
    public ICollection<string>? czNace { get; set; }

    /// <summary>Indeftifikátor sub-registru zdroje SZR - kód (ciselnikKod: SubRegistrSzr, zdroj:com)</summary>
    public string? subRegistrSzr { get; set; }

    /// <summary>Daňové identifikační číslo skupiny plátce DPH ve formátu CZNNNNNNNNNN</summary>
    public string? dicSkDph { get; set; }
}
