using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HledacFirmy.Ares;

public sealed class AresVrRoot
{
    [JsonPropertyName("pocetCelkem")]
    public decimal? PocetCelkem { get; set; }

    [JsonPropertyName("ekonomickeSubjekty")]
    public List<AresVrEkonomickeSubjekty>? EkonomickeSubjekty { get; set; }
}

public sealed class AresVrAdresa
{
    [JsonPropertyName("kodStatu")]
    public string? KodStatu { get; set; }

    [JsonPropertyName("nazevStatu")]
    public string? NazevStatu { get; set; }

    [JsonPropertyName("kodKraje")]
    public decimal? KodKraje { get; set; }

    [JsonPropertyName("nazevKraje")]
    public string? NazevKraje { get; set; }

    [JsonPropertyName("kodOkresu")]
    public decimal? KodOkresu { get; set; }

    [JsonPropertyName("nazevOkresu")]
    public string? NazevOkresu { get; set; }

    [JsonPropertyName("kodObce")]
    public decimal? KodObce { get; set; }

    [JsonPropertyName("nazevObce")]
    public string? NazevObce { get; set; }

    [JsonPropertyName("kodSpravnihoObvodu")]
    public decimal? KodSpravnihoObvodu { get; set; }

    [JsonPropertyName("nazevSpravnihoObvodu")]
    public string? NazevSpravnihoObvodu { get; set; }

    [JsonPropertyName("kodMestskehoObvodu")]
    public decimal? KodMestskehoObvodu { get; set; }

    [JsonPropertyName("nazevMestskehoObvodu")]
    public string? NazevMestskehoObvodu { get; set; }

    [JsonPropertyName("kodMestskeCastiObvodu")]
    public decimal? KodMestskeCastiObvodu { get; set; }

    [JsonPropertyName("kodUlice")]
    public decimal? KodUlice { get; set; }

    [JsonPropertyName("nazevMestskeCastiObvodu")]
    public string? NazevMestskeCastiObvodu { get; set; }

    [JsonPropertyName("nazevUlice")]
    public string? NazevUlice { get; set; }

    [JsonPropertyName("cisloDomovni")]
    public decimal? CisloDomovni { get; set; }

    [JsonPropertyName("doplnekAdresy")]
    public string? DoplnekAdresy { get; set; }

    [JsonPropertyName("kodCastiObce")]
    public decimal? KodCastiObce { get; set; }

    [JsonPropertyName("cisloOrientacni")]
    public decimal? CisloOrientacni { get; set; }

    [JsonPropertyName("cisloOrientacniPismeno")]
    public string? CisloOrientacniPismeno { get; set; }

    [JsonPropertyName("nazevCastiObce")]
    public string? NazevCastiObce { get; set; }

    [JsonPropertyName("kodAdresnihoMista")]
    public decimal? KodAdresnihoMista { get; set; }

    [JsonPropertyName("psc")]
    public decimal? Psc { get; set; }

    [JsonPropertyName("textovaAdresa")]
    public string? TextovaAdresa { get; set; }

    [JsonPropertyName("cisloDoAdresy")]
    public string? CisloDoAdresy { get; set; }

    [JsonPropertyName("typCisloDomovni")]
    public string? TypCisloDomovni { get; set; }

    [JsonPropertyName("standardizaceAdresy")]
    public bool StandardizaceAdresy { get; set; }

    [JsonPropertyName("pscTxt")]
    public string? PscTxt { get; set; }
}

public sealed class AresVrAdresy
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("adresa")]
    public AresVrAdresa? Adresa { get; set; }

    [JsonPropertyName("typAdresy")]
    public string? TypAdresy { get; set; }
}

public sealed class AresVrAkcie
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("typAkcie")]
    public string? TypAkcie { get; set; }

    [JsonPropertyName("podobaAkcie")]
    public string? PodobaAkcie { get; set; }

    [JsonPropertyName("pocet")]
    public decimal? Pocet { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("hodnota")]
    public AresVrHodnota? Hodnota { get; set; }
}

public sealed class AresVrBydliste
{
    [JsonPropertyName("kodStatu")]
    public string? KodStatu { get; set; }

    [JsonPropertyName("nazevStatu")]
    public string? NazevStatu { get; set; }

    [JsonPropertyName("kodKraje")]
    public decimal? KodKraje { get; set; }

    [JsonPropertyName("nazevKraje")]
    public string? NazevKraje { get; set; }

    [JsonPropertyName("kodOkresu")]
    public decimal? KodOkresu { get; set; }

    [JsonPropertyName("nazevOkresu")]
    public string? NazevOkresu { get; set; }

    [JsonPropertyName("kodObce")]
    public decimal? KodObce { get; set; }

    [JsonPropertyName("nazevObce")]
    public string? NazevObce { get; set; }

    [JsonPropertyName("kodSpravnihoObvodu")]
    public decimal? KodSpravnihoObvodu { get; set; }

    [JsonPropertyName("nazevSpravnihoObvodu")]
    public string? NazevSpravnihoObvodu { get; set; }

    [JsonPropertyName("kodMestskehoObvodu")]
    public decimal? KodMestskehoObvodu { get; set; }

    [JsonPropertyName("nazevMestskehoObvodu")]
    public string? NazevMestskehoObvodu { get; set; }

    [JsonPropertyName("kodMestskeCastiObvodu")]
    public decimal? KodMestskeCastiObvodu { get; set; }

    [JsonPropertyName("kodUlice")]
    public decimal? KodUlice { get; set; }

    [JsonPropertyName("nazevMestskeCastiObvodu")]
    public string? NazevMestskeCastiObvodu { get; set; }

    [JsonPropertyName("nazevUlice")]
    public string? NazevUlice { get; set; }

    [JsonPropertyName("cisloDomovni")]
    public decimal? CisloDomovni { get; set; }

    [JsonPropertyName("doplnekAdresy")]
    public string? DoplnekAdresy { get; set; }

    [JsonPropertyName("kodCastiObce")]
    public decimal? KodCastiObce { get; set; }

    [JsonPropertyName("cisloOrientacni")]
    public decimal? CisloOrientacni { get; set; }

    [JsonPropertyName("cisloOrientacniPismeno")]
    public string? CisloOrientacniPismeno { get; set; }

    [JsonPropertyName("nazevCastiObce")]
    public string? NazevCastiObce { get; set; }

    [JsonPropertyName("kodAdresnihoMista")]
    public decimal? KodAdresnihoMista { get; set; }

    [JsonPropertyName("psc")]
    public decimal? Psc { get; set; }

    [JsonPropertyName("textovaAdresa")]
    public string? TextovaAdresa { get; set; }

    [JsonPropertyName("cisloDoAdresy")]
    public string? CisloDoAdresy { get; set; }

    [JsonPropertyName("typCisloDomovni")]
    public string? TypCisloDomovni { get; set; }

    [JsonPropertyName("standardizaceAdresy")]
    public bool StandardizaceAdresy { get; set; }

    [JsonPropertyName("pscTxt")]
    public string? PscTxt { get; set; }

    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("adresa")]
    public AresVrAdresa? Adresa { get; set; }

    [JsonPropertyName("typAdresy")]
    public string? TypAdresy { get; set; }
}

public sealed class AresVrCinnosti
{
    [JsonPropertyName("predmetPodnikani")]
    public List<AresVrPredmetPodnikani>? PredmetPodnikani { get; set; }

    [JsonPropertyName("doplnkovaCinnost")]
    public List<AresVrDoplnkovaCinnost>? DoplnkovaCinnost { get; set; }

    [JsonPropertyName("predmetCinnosti")]
    public List<AresVrPredmetCinnosti>? PredmetCinnosti { get; set; }

    [JsonPropertyName("ucel")]
    public List<AresVrUcel>? Ucel { get; set; }
}

public sealed class AresVrClenoveOrganu
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("skrytyUdaj")]
    public AresVrSkrytyUdaj? SkrytyUdaj { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }

    [JsonPropertyName("clenstvi")]
    public AresVrClenstvi? Clenstvi { get; set; }

    [JsonPropertyName("nazevAngazma")]
    public string? NazevAngazma { get; set; }

    [JsonPropertyName("pravnickaOsoba")]
    public AresVrPravnickaOsoba? PravnickaOsoba { get; set; }

    [JsonPropertyName("fyzickaOsoba")]
    public AresVrFyzickaOsoba? FyzickaOsoba { get; set; }
}

public sealed class AresVrClenstvi
{
    [JsonPropertyName("textZaOsobu")]
    public string? TextZaOsobu { get; set; }

    [JsonPropertyName("clenstvi")]
    public AresVrClenstvi? Clenstvi { get; set; }

    [JsonPropertyName("funkce")]
    public AresVrFunkce? Funkce { get; set; }

    [JsonPropertyName("textZruseni")]
    public string? TextZruseni { get; set; }

    [JsonPropertyName("vznikClenstvi")]
    public string? VznikClenstvi { get; set; }

    [JsonPropertyName("zanikClenstvi")]
    public string? ZanikClenstvi { get; set; }
}

public sealed class AresVrDatumVzniku
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrDoplnkovaCinnost
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrEkonomickeSubjekty
{
    [JsonPropertyName("icoId")]
    public string? IcoId { get; set; }

    [JsonPropertyName("zaznamy")]
    public List<AresVrZaznamy>? Zaznamy { get; set; }
}

public sealed class AresVrExekuce
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrFunkce
{
    [JsonPropertyName("vznikFunkce")]
    public string? VznikFunkce { get; set; }

    [JsonPropertyName("zanikFunkce")]
    public string? ZanikFunkce { get; set; }

    [JsonPropertyName("nazev")]
    public string? Nazev { get; set; }
}

public sealed class AresVrFyzickaOsoba
{
    [JsonPropertyName("textOsoba")]
    public string? TextOsoba { get; set; }

    [JsonPropertyName("textOsobaOd")]
    public string? TextOsobaOd { get; set; }

    [JsonPropertyName("adresa")]
    public AresVrAdresa? Adresa { get; set; }

    [JsonPropertyName("textOsobaDo")]
    public string? TextOsobaDo { get; set; }

    [JsonPropertyName("bydliste")]
    public AresVrBydliste? Bydliste { get; set; }

    [JsonPropertyName("datumNarozeni")]
    public DateTime? DatumNarozeni { get; set; }

    [JsonPropertyName("jmeno")]
    public string? Jmeno { get; set; }

    [JsonPropertyName("prijmeni")]
    public string? Prijmeni { get; set; }

    [JsonPropertyName("statniObcanstvi")]
    public string? StatniObcanstvi { get; set; }

    [JsonPropertyName("titulPredJmenem")]
    public string? TitulPredJmenem { get; set; }

    [JsonPropertyName("titulZaJmenem")]
    public string? TitulZaJmenem { get; set; }
}

public sealed class AresVrHodnota
{
    [JsonPropertyName("typObnos")]
    public string? TypObnos { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrIco
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrInsolvence
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("spravce")]
    public List<AresVrSpravce>? Spravce { get; set; }

    [JsonPropertyName("insolvencniZapis")]
    public List<AresVrInsolvencniZapis>? InsolvencniZapis { get; set; }
}

public sealed class AresVrInsolvencniZapis
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("typZapisu")]
    public string? TypZapisu { get; set; }
}

public sealed class AresVrKategorieZO
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("typKategorieZo")]
    public string? TypKategorieZo { get; set; }
}

public sealed class AresVrKonkursy
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("spravce")]
    public List<AresVrSpravce>? Spravce { get; set; }

    [JsonPropertyName("typKonkursu")]
    public string? TypKonkursu { get; set; }

    [JsonPropertyName("datumRozhodnutiOs")]
    public DateTime? DatumRozhodnutiOs { get; set; }

    [JsonPropertyName("datumVyveseni")]
    public DateTime? DatumVyveseni { get; set; }

    [JsonPropertyName("spisZnOs")]
    public string? SpisZnOs { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("zruseniKonkursu")]
    public List<AresVrZruseniKonkursu>? ZruseniKonkursu { get; set; }
}

public sealed class AresVrNazevNejvyssihoOrganu
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrObchodniJmeno
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrObchodniJmenoCizi
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }

    [JsonPropertyName("jazyk")]
    public string? Jazyk { get; set; }
}

public sealed class AresVrOdstepneZavody
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("vedouci")]
    public List<AresVrVedouci>? Vedouci { get; set; }

    [JsonPropertyName("ostatniSkutecnosti")]
    public List<AresVrOstatniSkutecnosti>? OstatniSkutecnosti { get; set; }

    [JsonPropertyName("cinnosti")]
    public AresVrCinnosti? Cinnosti { get; set; }

    [JsonPropertyName("ico")]
    public List<AresVrIco>? Ico { get; set; }

    [JsonPropertyName("sidlo")]
    public List<AresVrSidlo>? Sidlo { get; set; }

    [JsonPropertyName("pravniForma")]
    public List<AresVrPravniForma>? PravniForma { get; set; }

    [JsonPropertyName("obchodniJmeno")]
    public List<AresVrObchodniJmeno>? ObchodniJmeno { get; set; }
}

public sealed class AresVrOsoba
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("skrytyUdaj")]
    public AresVrSkrytyUdaj? SkrytyUdaj { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }

    [JsonPropertyName("clenstvi")]
    public AresVrClenstvi? Clenstvi { get; set; }

    [JsonPropertyName("nazevAngazma")]
    public string? NazevAngazma { get; set; }

    [JsonPropertyName("pravnickaOsoba")]
    public AresVrPravnickaOsoba? PravnickaOsoba { get; set; }

    [JsonPropertyName("fyzickaOsoba")]
    public AresVrFyzickaOsoba? FyzickaOsoba { get; set; }
}

public sealed class AresVrOsobaPodnikatel
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("clenstvi")]
    public AresVrClenstvi? Clenstvi { get; set; }

    [JsonPropertyName("fyzickaOsoba")]
    public AresVrFyzickaOsoba? FyzickaOsoba { get; set; }

    [JsonPropertyName("nazevAngazma")]
    public string? NazevAngazma { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }
}

public sealed class AresVrOstatniOrgany
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("nazevOrganu")]
    public string? NazevOrganu { get; set; }

    [JsonPropertyName("pocetClenu")]
    public List<AresVrPocetClenu>? PocetClenu { get; set; }

    [JsonPropertyName("clenoveOrganu")]
    public List<AresVrClenoveOrganu>? ClenoveOrganu { get; set; }

    [JsonPropertyName("typOrganu")]
    public string? TypOrganu { get; set; }

    [JsonPropertyName("nazevAngazma")]
    public string? NazevAngazma { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }
}

public sealed class AresVrOstatniSkutecnosti
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrPobyt
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("adresa")]
    public AresVrAdresa? Adresa { get; set; }

    [JsonPropertyName("typAdresy")]
    public string? TypAdresy { get; set; }
}

public sealed class AresVrPocetClenu
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("pocetClenu")]
    public decimal? PocetClenu { get; set; }

    [JsonPropertyName("maxPocetClenu")]
    public decimal? MaxPocetClenu { get; set; }

    [JsonPropertyName("typ")]
    public string? Typ { get; set; }
}

public sealed class AresVrPodil
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("vklad")]
    public AresVrVklad? Vklad { get; set; }

    [JsonPropertyName("velikostPodilu")]
    public AresVrVelikostPodilu? VelikostPodilu { get; set; }

    [JsonPropertyName("splaceni")]
    public AresVrSplaceni? Splaceni { get; set; }

    [JsonPropertyName("zastavniPravo")]
    public List<AresVrZastavniPravo>? ZastavniPravo { get; set; }
}

public sealed class AresVrPodilnik
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("skrytyUdaj")]
    public AresVrSkrytyUdaj? SkrytyUdaj { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }

    [JsonPropertyName("clenstvi")]
    public AresVrClenstvi? Clenstvi { get; set; }

    [JsonPropertyName("nazevAngazma")]
    public string? NazevAngazma { get; set; }

    [JsonPropertyName("pravnickaOsoba")]
    public AresVrPravnickaOsoba? PravnickaOsoba { get; set; }

    [JsonPropertyName("fyzickaOsoba")]
    public AresVrFyzickaOsoba? FyzickaOsoba { get; set; }
}

public sealed class AresVrPodnikatel
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("bydliste")]
    public List<AresVrBydliste>? Bydliste { get; set; }

    [JsonPropertyName("osobaPodnikatel")]
    public AresVrOsobaPodnikatel? OsobaPodnikatel { get; set; }

    [JsonPropertyName("pobyt")]
    public List<AresVrPobyt>? Pobyt { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }

    [JsonPropertyName("nazevAngazma")]
    public string? NazevAngazma { get; set; }
}

public sealed class AresVrPravnickaOsoba
{
    [JsonPropertyName("textOsoba")]
    public string? TextOsoba { get; set; }

    [JsonPropertyName("textOsobaOd")]
    public string? TextOsobaOd { get; set; }

    [JsonPropertyName("adresa")]
    public AresVrAdresa? Adresa { get; set; }

    [JsonPropertyName("textOsobaDo")]
    public string? TextOsobaDo { get; set; }

    [JsonPropertyName("ico")]
    public string? Ico { get; set; }

    [JsonPropertyName("obchodniJmeno")]
    public string? ObchodniJmeno { get; set; }

    [JsonPropertyName("zastoupeni")]
    public List<AresVrZastoupeni>? Zastoupeni { get; set; }

    [JsonPropertyName("pravniForma")]
    public string? PravniForma { get; set; }
}

public sealed class AresVrPravniDuvodVymazu
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrPravniForma
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrPredmetCinnosti
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrPredmetPodnikani
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrSidlo
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("adresa")]
    public AresVrAdresa? Adresa { get; set; }

    [JsonPropertyName("typAdresy")]
    public string? TypAdresy { get; set; }
}

public sealed class AresVrSkrytyUdaj
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrSpisovaZnacka
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("soud")]
    public string? Soud { get; set; }

    [JsonPropertyName("oddil")]
    public string? Oddil { get; set; }

    [JsonPropertyName("vlozka")]
    public decimal? Vlozka { get; set; }
}

public sealed class AresVrSplaceni
{
    [JsonPropertyName("typObnos")]
    public string? TypObnos { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrSpolecnici
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("nazevOrganu")]
    public string? NazevOrganu { get; set; }

    [JsonPropertyName("spolecnik")]
    public List<AresVrSpolecnik>? Spolecnik { get; set; }

    [JsonPropertyName("spolecnyPodil")]
    public List<AresVrSpolecnyPodil>? SpolecnyPodil { get; set; }

    [JsonPropertyName("typOrganu")]
    public string? TypOrganu { get; set; }

    [JsonPropertyName("uvolnenyPodil")]
    public List<AresVrUvolnenyPodil>? UvolnenyPodil { get; set; }

    [JsonPropertyName("nazev")]
    public string? Nazev { get; set; }
}

public sealed class AresVrSpolecnik
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("podil")]
    public AresVrPodil? Podil { get; set; }

    [JsonPropertyName("osoba")]
    public AresVrOsoba? Osoba { get; set; }
}

public sealed class AresVrSpolecnyPodil
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("podilnik")]
    public List<AresVrPodilnik>? Podilnik { get; set; }

    [JsonPropertyName("podil")]
    public AresVrPodil? Podil { get; set; }
}

public sealed class AresVrSpravce
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("skrytyUdaj")]
    public AresVrSkrytyUdaj? SkrytyUdaj { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }

    [JsonPropertyName("clenstvi")]
    public AresVrClenstvi? Clenstvi { get; set; }

    [JsonPropertyName("nazevAngazma")]
    public string? NazevAngazma { get; set; }

    [JsonPropertyName("pravnickaOsoba")]
    public AresVrPravnickaOsoba? PravnickaOsoba { get; set; }

    [JsonPropertyName("fyzickaOsoba")]
    public AresVrFyzickaOsoba? FyzickaOsoba { get; set; }
}

public sealed class AresVrStatutarniOrgany
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("nazevOrganu")]
    public string? NazevOrganu { get; set; }

    [JsonPropertyName("pocetClenu")]
    public List<AresVrPocetClenu>? PocetClenu { get; set; }

    [JsonPropertyName("clenoveOrganu")]
    public List<AresVrClenoveOrganu>? ClenoveOrganu { get; set; }

    [JsonPropertyName("typOrganu")]
    public string? TypOrganu { get; set; }

    [JsonPropertyName("nazevAngazma")]
    public string? NazevAngazma { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }

    [JsonPropertyName("zpusobJednani")]
    public List<AresVrZpusobJednani>? ZpusobJednani { get; set; }
}

public sealed class AresVrUcel
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrUvolnenyPodil
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("clenstvi")]
    public AresVrClenstvi? Clenstvi { get; set; }

    [JsonPropertyName("podil")]
    public AresVrPodil? Podil { get; set; }
}

public sealed class AresVrVedouci
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("skrytyUdaj")]
    public AresVrSkrytyUdaj? SkrytyUdaj { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }

    [JsonPropertyName("clenstvi")]
    public AresVrClenstvi? Clenstvi { get; set; }

    [JsonPropertyName("nazevAngazma")]
    public string? NazevAngazma { get; set; }

    [JsonPropertyName("pravnickaOsoba")]
    public AresVrPravnickaOsoba? PravnickaOsoba { get; set; }

    [JsonPropertyName("fyzickaOsoba")]
    public AresVrFyzickaOsoba? FyzickaOsoba { get; set; }
}

public sealed class AresVrVelikostPodilu
{
    [JsonPropertyName("typObnos")]
    public string? TypObnos { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrVklad
{
    [JsonPropertyName("typObnos")]
    public string? TypObnos { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrVklady
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("vklad")]
    public AresVrVklad? Vklad { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("typVkladu")]
    public string? TypVkladu { get; set; }
}

public sealed class AresVrZakladniKapital
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("typJmeni")]
    public string? TypJmeni { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("vklad")]
    public AresVrVklad? Vklad { get; set; }

    [JsonPropertyName("splaceni")]
    public AresVrSplaceni? Splaceni { get; set; }
}

public sealed class AresVrZastavniPravo
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("vznikZastavnihoPrava")]
    public string? VznikZastavnihoPrava { get; set; }

    [JsonPropertyName("zanikZastavnihoPrava")]
    public string? ZanikZastavnihoPrava { get; set; }
}

public sealed class AresVrZastoupeni
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("clenstvi")]
    public AresVrClenstvi? Clenstvi { get; set; }

    [JsonPropertyName("fyzickaOsoba")]
    public AresVrFyzickaOsoba? FyzickaOsoba { get; set; }

    [JsonPropertyName("nazevAngazma")]
    public string? NazevAngazma { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }
}

public sealed class AresVrZaznamy
{
    [JsonPropertyName("akcie")]
    public List<AresVrAkcie>? Akcie { get; set; }

    [JsonPropertyName("rejstrik")]
    public string? Rejstrik { get; set; }

    [JsonPropertyName("primarniZaznam")]
    public bool PrimarniZaznam { get; set; }

    [JsonPropertyName("spisovaZnacka")]
    public List<AresVrSpisovaZnacka>? SpisovaZnacka { get; set; }

    [JsonPropertyName("ico")]
    public List<AresVrIco>? Ico { get; set; }

    [JsonPropertyName("obchodniJmeno")]
    public List<AresVrObchodniJmeno>? ObchodniJmeno { get; set; }

    [JsonPropertyName("vklady")]
    public List<AresVrVklady>? Vklady { get; set; }

    [JsonPropertyName("obchodniJmenoCizi")]
    public List<AresVrObchodniJmenoCizi>? ObchodniJmenoCizi { get; set; }

    [JsonPropertyName("zakladniKapital")]
    public List<AresVrZakladniKapital>? ZakladniKapital { get; set; }

    [JsonPropertyName("pravniForma")]
    public List<AresVrPravniForma>? PravniForma { get; set; }

    [JsonPropertyName("financniUrad")]
    public string? FinancniUrad { get; set; }

    [JsonPropertyName("adresy")]
    public List<AresVrAdresy>? Adresy { get; set; }

    [JsonPropertyName("ostatniSkutecnosti")]
    public List<AresVrOstatniSkutecnosti>? OstatniSkutecnosti { get; set; }

    [JsonPropertyName("datumAktualizace")]
    public DateTime? DatumAktualizace { get; set; }

    [JsonPropertyName("stavSubjektu")]
    public string? StavSubjektu { get; set; }

    [JsonPropertyName("datumVzniku")]
    public List<AresVrDatumVzniku>? DatumVzniku { get; set; }

    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("zpusobRizeni")]
    public List<AresVrZpusobRizeni>? ZpusobRizeni { get; set; }

    [JsonPropertyName("kategorieZO")]
    public List<AresVrKategorieZO>? KategorieZO { get; set; }

    [JsonPropertyName("pravniDuvodVymazu")]
    public List<AresVrPravniDuvodVymazu>? PravniDuvodVymazu { get; set; }

    [JsonPropertyName("cinnosti")]
    public AresVrCinnosti? Cinnosti { get; set; }

    [JsonPropertyName("nazevNejvyssihoOrganu")]
    public List<AresVrNazevNejvyssihoOrganu>? NazevNejvyssihoOrganu { get; set; }

    [JsonPropertyName("exekuce")]
    public List<AresVrExekuce>? Exekuce { get; set; }

    [JsonPropertyName("ostatniOrgany")]
    public List<AresVrOstatniOrgany>? OstatniOrgany { get; set; }

    [JsonPropertyName("statutarniOrgany")]
    public List<AresVrStatutarniOrgany>? StatutarniOrgany { get; set; }

    [JsonPropertyName("podnikatel")]
    public List<AresVrPodnikatel>? Podnikatel { get; set; }

    [JsonPropertyName("spolecnici")]
    public List<AresVrSpolecnici>? Spolecnici { get; set; }

    [JsonPropertyName("odstepneZavody")]
    public List<AresVrOdstepneZavody>? OdstepneZavody { get; set; }

    [JsonPropertyName("insolvence")]
    public List<AresVrInsolvence>? Insolvence { get; set; }

    [JsonPropertyName("konkursy")]
    public List<AresVrKonkursy>? Konkursy { get; set; }
}

public sealed class AresVrZpusobJednani
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}

public sealed class AresVrZpusobRizeni
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("typ")]
    public string? Typ { get; set; }
}

public sealed class AresVrZruseniKonkursu
{
    [JsonPropertyName("datumZapisu")]
    public DateTime? DatumZapisu { get; set; }

    [JsonPropertyName("datumVymazu")]
    public DateTime? DatumVymazu { get; set; }

    [JsonPropertyName("hodnota")]
    public string? Hodnota { get; set; }
}
