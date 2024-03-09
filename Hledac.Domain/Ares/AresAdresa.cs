namespace Hledac.Domain.Ares;

/// <summary>
/// Adresa
/// </summary>
public sealed class AresAdresa
{
    /// <summary>Kód státu(ciselnikKod: Stat)</summary>
    public string? kodStatu { get; set; }

    /// <summary>Název státu</summary>
    public string? nazevStatu { get; set; }

    /// <summary>Kód kraje</summary>
    public long? kodKraje { get; set; }

    /// <summary>Název kraje</summary>
    public string? nazevKraje { get; set; }

    /// <summary>Kód okresu</summary>
    public long? kodOkresu { get; set; }

    /// <summary>Název okresu</summary>
    public string? nazevOkresu { get; set; }

    /// <summary>Kód obce</summary>
    public long? kodObce { get; set; }

    /// <summary>Název obce</summary>
    public string? nazevObce { get; set; }

    /// <summary>Kód správního obvodu Prahy</summary>
    public long? kodSpravnihoObvodu { get; set; }

    /// <summary>Název správního obvodu Prahy</summary>
    public string? nazevSpravnihoObvodu { get; set; }

    /// <summary>Kód městského obvodu Prahy</summary>
    public long? kodMestskehoObvodu { get; set; }

    /// <summary>Název městského obvodu Prahy</summary>
    public string? nazevMestskehoObvodu { get; set; }

    /// <summary>Kód městské části statutárního města</summary>
    public long? kodMestskeCastiObvodu { get; set; }

    /// <summary>Kód ulice, veřejného prostranství ze zdroje</summary>
    public long? kodUlice { get; set; }

    /// <summary>Název městské části statutárního města</summary>
    public string? nazevMestskeCastiObvodu { get; set; }

    /// <summary>Název ulice, veřejného prostranství</summary>
    public string? nazevUlice { get; set; }

    /// <summary>Číslo domovní</summary>
    public long? cisloDomovni { get; set; }

    /// <summary>Doplňující informace adresního popisu</summary>
    public string? doplnekAdresy{ get; set; }

    /// <summary>Kód časti obce</summary>
    public long? kodCastiObce { get; set; }

    /// <summary>Číslo orientační - číselná část</summary>
    public long? cisloOrientacni { get; set; }

    /// <summary>Číslo orientační - písmenná část</summary>
    public string? cisloOrientacniPismeno { get; set; }

    /// <summary>Název části obce</summary>
    public string? nazevCastiObce { get; set; }

    /// <summary>Kód adresního místa</summary>
    public long? kodAdresnihoMista { get; set; }

    /// <summary>Poštovní směrovací číslo adresní pošty</summary>
    public long? psc { get; set; }

    /// <summary>Nestrukturovaná adresa(formátovaná adresa)</summary>
    public string? textovaAdresa { get; set; }

    /// <summary>Nestrukturované číslo/a použíté v adrese</summary>
    public string? cisloDoAdresy { get; set; }

    /// <summary>Typ čísla domu (ciselnikKod: TypCislaDomovniho)</summary>
    public string? typCisloDomovni { get; set; }

    /// <summary>Stav standardizace adresy dle RÚIAN</summary>
    public bool? standardizaceAdresy { get; set; }

    /// <summary>Psč zahraničních nebo nestandardně definovaných čísel</summary>
    public string? pscTxt { get; set; }
}
