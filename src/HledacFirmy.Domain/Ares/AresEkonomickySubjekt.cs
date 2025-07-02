using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HledacFirmy.Ares;

/// <summary>
/// Základní informace o ekonomickém subjektu
/// </summary>
public sealed class AresEkonomickySubjekt
{
    /// <summary>Identifikační číslo osoby - IČO</summary>
    [JsonPropertyName("ico")]
    public string? Ico { get; set; }

    /// <summary>Obchodní jméno ekonomického subjektu</summary>
    [JsonPropertyName("obchodniJmeno")]
    public string? ObchodniJmeno { get; set; }

    [JsonPropertyName("sidlo")]
    public AresAdresa? Sidlo { get; set; }

    /// <summary>Právní forma - kód(ciselnikKod: PravniForma, zdroj: res, com)</summary>
    [JsonPropertyName("pravniForma")]
    public string? PravniForma { get; set; }

    /// <summary>Správně příslušný finanční úřad - kód(ciselnikKod: FinancniUrad, zdroj:ufo)</summary>
    [JsonPropertyName("financniUrad")]
    public string? FinancniUrad { get; set; }

    /// <summary>Datum vzniku ekonomického subjektu</summary>
    [JsonPropertyName("datumVzniku")]
    public DateTime? DatumVzniku { get; set; }

    /// <summary>Datum zániku ekonomického subjektu</summary>
    [JsonPropertyName("datumZaniku")]
    public DateTime? DatumZaniku { get; set; }

    /// <summary>Datum aktualizace záznamu</summary>
    [JsonPropertyName("datumAktualizace")]
    public DateTime? DatumAktualizace { get; set; }

    /// <summary>Daňové identifikační číslo ve formátu CZNNNNNNNNNN</summary>
    [JsonPropertyName("dic")]
    public string? Dic { get; set; }

    /// <summary>Ičo ekonomického subjektu, pokud je ičo přidělené. Id ekonomického subjektu, pokud je ičo nepřidělené.</summary>
    [JsonPropertyName("icoId")]
    public string? IcoId { get; set; }

    /// <summary>Adresa doručovací dle vyhlášky 359/2011 sb.</summary>
    [JsonPropertyName("adresaDorucovaci")]
    public AresAdresaDorucovaci? AdresaDorucovaci { get; set; }

    [JsonPropertyName("seznamRegistraci")]
    public AresSeznamRegistraci? SeznamRegistraci { get; set; }

    /// <summary>Identifikace primárního zdroje dat.</summary>
    [JsonPropertyName("primarniZdroj")]
    public string? PrimarniZdroj { get; set; }

    [JsonPropertyName("dalsiUdaje")]
    public List<AresDalsiUdaje>? DalsiUdaje { get; set; }

    /// <summary>CZ-NACE ekonomického subjektu – kód (ciselnikKod: CzNace, zdroj:res)</summary>
    [JsonPropertyName("czNace")]
    public List<string>? CzNace { get; set; }

    /// <summary>Indeftifikátor sub-registru zdroje SZR - kód (ciselnikKod: SubRegistrSzr, zdroj:com)</summary>
    [JsonPropertyName("subRegistrSzr")]
    public string? SubRegistrSzr { get; set; }

    /// <summary>Daňové identifikační číslo skupiny plátce DPH ve formátu CZNNNNNNNNNN</summary>
    [JsonPropertyName("dicSkDph")]
    public string? DicSkDph { get; set; }
}

/// <summary>
/// Adresa doručovací dle vyhlášky 359/2011 sb.
/// </summary>
public sealed class AresAdresaDorucovaci
{
    /// <summary>řádek doručovací adresy</summary>
    [JsonPropertyName("radekAdresy1")]
    public string? RadekAdresy1 { get; set; }

    /// <summary>řádek doručovací adresy</summary>
    [JsonPropertyName("radekAdresy2")]
    public string? RadekAdresy2 { get; set; }

    /// <summary>řádek doručovací adresy</summary>
    [JsonPropertyName("radekAdresy3")]
    public string? RadekAdresy3 { get; set; }
}

/// <summary>
/// Další údaje o ekonomickém subjektu.
/// </summary>
public sealed class AresDalsiUdaje
{
    [JsonPropertyName("obchodniJmeno")]
    public List<AresObchodniJmeno>? ObchodniJmeno { get; set; }

    [JsonPropertyName("sidlo")]
    public List<AresAdresa>? Sidlo { get; set; }

    /// <summary>Právní forma - kód (ciselnikKod: PravniForma, zdroj: res, com)</summary>
    [JsonPropertyName("pravniForma")]
    public string? PravniForma { get; set; }

    /// <summary>Aktuální spisová značka ve tvaru ODDIL xx/SOUD (např. B 100/MSPH) - poskytováno pouze pro zdroj: Veřejné rejstříky</summary>
    [JsonPropertyName("spisovaZnacka")]
    public string? SpisovaZnacka { get; set; }

    /// <summary>Identifikace primárního zdroje dat - kód (ciselnikKod: TypZdroje, zdroj: com)</summary>
    [JsonPropertyName("datovyZdroj")]
    public string? DatovyZdroj { get; set; }
}

/// <summary>
/// Obchodní jméno s platností.
/// </summary>
public sealed class AresObchodniJmeno
{
    /// <summary>Platnost údaje od data</summary>
    [JsonPropertyName("platnostOd")]
    public DateTime? PlatnostOd { get; set; }

    /// <summary>Platnost údaje do data</summary>
    [JsonPropertyName("platnostDo")]
    public DateTime? PlatnostDo { get; set; }

    /// <summary>Obchodní jméno ekonomického subjektu</summary>
    [JsonPropertyName("obchodniJmeno")]
    public string? ObchodniJmeno { get; set; }

    /// <summary>Primární záznam</summary>
    [JsonPropertyName("primarniZaznam")]
    public bool? PrimarniZaznam { get; set; }
}

/// <summary>
/// Seznam registrací ekonomického subjektu v jednotlivých zdrojích
/// </summary>
public sealed class AresSeznamRegistraci
{
    /// <summary>Stav ekonomického subjektu ve zdroji VR (Veřejné rejstříky) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeVr")]
    public string? StavZdrojeVr { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RES (Registr ekonomických subjektů) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeRes")]
    public string? StavZdrojeRes { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RŽP (Registr živnostenského podnikání) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeRzp")]
    public string? StavZdrojeRzp { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji NRPZS (Národní registr poskytovatelů zdrovotnických služeb) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeNrpzs")]
    public string? StavZdrojeNrpzs { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RPSH (Registr politických stran a hnutí) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeRpsh")]
    public string? StavZdrojeRpsh { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RCNS(Registr církví a náboženských společenství) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeRcns")]
    public string? StavZdrojeRcns { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji SZR(Společný zemědělský registr) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeSzr")]
    public string? StavZdrojeSzr { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji DPH(Registr plátců daně s přidané hodnoty) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeDph")]
    public string? StavZdrojeDph { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji SD(Registr plátců spotřební daně) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeSd")]
    public string? StavZdrojeSd { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji ISIR(Insolvenční rejstřík) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeIr")]
    public string? StavZdrojeIr { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji CEÚ(Centrální evidence úpadců) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeCeu")]
    public string? StavZdrojeCeu { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RŠ(Registr škol) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeRs")]
    public string? StavZdrojeRs { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RED(Registr evidence dotací) - kód (ciselnikKod: StavZdroje, zdroj: com)</summary>
    [JsonPropertyName("stavZdrojeRed")]
    public string? StavZdrojeRed { get; set; }
}

/// <summary>
/// Adresa
/// </summary>
public sealed class AresAdresa
{
    /// <summary>Kód státu(ciselnikKod: Stat)</summary>
    [JsonPropertyName("kodStatu")]
    public string? KodStatu { get; set; }

    /// <summary>Název státu</summary>
    [JsonPropertyName("nazevStatu")]
    public string? NazevStatu { get; set; }

    /// <summary>Kód kraje</summary>
    [JsonPropertyName("kodKraje")]
    public long? KodKraje { get; set; }

    /// <summary>Název kraje</summary>
    [JsonPropertyName("nazevKraje")]
    public string? NazevKraje { get; set; }

    /// <summary>Kód okresu</summary>
    [JsonPropertyName("kodOkresu")]
    public long? KodOkresu { get; set; }

    /// <summary>Název okresu</summary>
    [JsonPropertyName("nazevOkresu")]
    public string? NazevOkresu { get; set; }

    /// <summary>Kód obce</summary>
    [JsonPropertyName("kodObce")]
    public long? KodObce { get; set; }

    /// <summary>Název obce</summary>
    [JsonPropertyName("nazevObce")]
    public string? NazevObce { get; set; }

    /// <summary>Kód správního obvodu Prahy</summary>
    [JsonPropertyName("kodSpravnihoObvodu")]
    public long? KodSpravnihoObvodu { get; set; }

    /// <summary>Název správního obvodu Prahy</summary>
    [JsonPropertyName("nazevSpravnihoObvodu")]
    public string? NazevSpravnihoObvodu { get; set; }

    /// <summary>Kód městského obvodu Prahy</summary>
    [JsonPropertyName("kodMestskehoObvodu")]
    public long? KodMestskehoObvodu { get; set; }

    /// <summary>Název městského obvodu Prahy</summary>
    [JsonPropertyName("nazevMestskehoObvodu")]
    public string? NazevMestskehoObvodu { get; set; }

    /// <summary>Kód městské části statutárního města</summary>
    [JsonPropertyName("kodMestskeCastiObvodu")]
    public long? KodMestskeCastiObvodu { get; set; }

    /// <summary>Kód ulice, veřejného prostranství ze zdroje</summary>
    [JsonPropertyName("kodUlice")]
    public long? KodUlice { get; set; }

    /// <summary>Název městské části statutárního města</summary>
    [JsonPropertyName("nazevMestskeCastiObvodu")]
    public string? NazevMestskeCastiObvodu { get; set; }

    /// <summary>Název ulice, veřejného prostranství</summary>
    [JsonPropertyName("nazevUlice")]
    public string? NazevUlice { get; set; }

    /// <summary>Číslo domovní</summary>
    [JsonPropertyName("cisloDomovni")]
    public long? CisloDomovni { get; set; }

    /// <summary>Doplňující informace adresního popisu</summary>
    [JsonPropertyName("doplnekAdresy")]
    public string? DoplnekAdresy { get; set; }

    /// <summary>Kód časti obce</summary>
    [JsonPropertyName("kodCastiObce")]
    public long? KodCastiObce { get; set; }

    /// <summary>Číslo orientační - číselná část</summary>
    [JsonPropertyName("cisloOrientacni")]
    public long? CisloOrientacni { get; set; }

    /// <summary>Číslo orientační - písmenná část</summary>
    [JsonPropertyName("cisloOrientacniPismeno")]
    public string? CisloOrientacniPismeno { get; set; }

    /// <summary>Název části obce</summary>
    [JsonPropertyName("nazevCastiObce")]
    public string? NazevCastiObce { get; set; }

    /// <summary>Kód adresního místa</summary>
    [JsonPropertyName("kodAdresnihoMista")]
    public long? KodAdresnihoMista { get; set; }

    /// <summary>Poštovní směrovací číslo adresní pošty</summary>
    [JsonPropertyName("psc")]
    public long? Psc { get; set; }

    /// <summary>Nestrukturovaná adresa(formátovaná adresa)</summary>
    [JsonPropertyName("textovaAdresa")]
    public string? TextovaAdresa { get; set; }

    /// <summary>Nestrukturované číslo/a použíté v adrese</summary>
    [JsonPropertyName("cisloDoAdresy")]
    public string? CisloDoAdresy { get; set; }

    /// <summary>Typ čísla domu (ciselnikKod: TypCislaDomovniho)</summary>
    [JsonPropertyName("typCisloDomovni")]
    public long? TypCisloDomovni { get; set; }

    /// <summary>Stav standardizace adresy dle RÚIAN</summary>
    [JsonPropertyName("standardizaceAdresy")]
    public bool? StandardizaceAdresy { get; set; }

    /// <summary>Psč zahraničních nebo nestandardně definovaných čísel</summary>
    [JsonPropertyName("pscTxt")]
    public string? PscTxt { get; set; }
}
