using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HledacFirmy.Ares;

public sealed class AresRZP
{
    [JsonPropertyName("icoId")]
    public string? IcoId { get; set; }

    [JsonPropertyName("zaznamy")]
    public List<Zaznamy>? Zaznamy { get; set; }
}

public class AdresySubjektu
{
    [JsonPropertyName("kodStatu")]
    public string? KodStatu { get; set; }

    [JsonPropertyName("nazevStatu")]
    public string? NazevStatu { get; set; }

    [JsonPropertyName("kodKraje")]
    public int? KodKraje { get; set; }

    [JsonPropertyName("nazevKraje")]
    public string? NazevKraje { get; set; }

    [JsonPropertyName("kodOkresu")]
    public int? KodOkresu { get; set; }

    [JsonPropertyName("nazevOkresu")]
    public string? NazevOkresu { get; set; }

    [JsonPropertyName("kodObce")]
    public int? KodObce { get; set; }

    [JsonPropertyName("nazevObce")]
    public string? NazevObce { get; set; }

    [JsonPropertyName("kodSpravnihoObvodu")]
    public int? KodSpravnihoObvodu { get; set; }

    [JsonPropertyName("nazevSpravnihoObvodu")]
    public string? NazevSpravnihoObvodu { get; set; }

    [JsonPropertyName("kodMestskehoObvodu")]
    public int? KodMestskehoObvodu { get; set; }

    [JsonPropertyName("nazevMestskehoObvodu")]
    public string? NazevMestskehoObvodu { get; set; }

    [JsonPropertyName("kodMestskeCastiObvodu")]
    public int? KodMestskeCastiObvodu { get; set; }

    [JsonPropertyName("kodUlice")]
    public int? KodUlice { get; set; }

    [JsonPropertyName("nazevMestskeCastiObvodu")]
    public string? NazevMestskeCastiObvodu { get; set; }

    [JsonPropertyName("nazevUlice")]
    public string? NazevUlice { get; set; }

    [JsonPropertyName("cisloDomovni")]
    public int? CisloDomovni { get; set; }

    [JsonPropertyName("doplnekAdresy")]
    public string? DoplnekAdresy { get; set; }

    [JsonPropertyName("kodCastiObce")]
    public int? KodCastiObce { get; set; }

    [JsonPropertyName("cisloOrientacni")]
    public int? CisloOrientacni { get; set; }

    [JsonPropertyName("cisloOrientacniPismeno")]
    public string? CisloOrientacniPismeno { get; set; }

    [JsonPropertyName("nazevCastiObce")]
    public string? NazevCastiObce { get; set; }

    [JsonPropertyName("kodAdresnihoMista")]
    public int? KodAdresnihoMista { get; set; }

    [JsonPropertyName("psc")]
    public int? Psc { get; set; }

    [JsonPropertyName("textovaAdresa")]
    public string? TextovaAdresa { get; set; }

    [JsonPropertyName("cisloDoAdresy")]
    public string? CisloDoAdresy { get; set; }

    [JsonPropertyName("typCisloDomovni")]
    public string? TypCisloDomovni { get; set; }

    [JsonPropertyName("standardizaceAdresy")]
    public bool? StandardizaceAdresy { get; set; }

    [JsonPropertyName("pscTxt")]
    public string? PscTxt { get; set; }

    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("typAdresy")]
    public string? TypAdresy { get; set; }
}

public class AngazovaneOsoby
{
    [JsonPropertyName("jmeno")]
    public string? Jmeno { get; set; }

    [JsonPropertyName("prijmeni")]
    public string? Prijmeni { get; set; }

    [JsonPropertyName("titulPredJmenem")]
    public string? TitulPredJmenem { get; set; }

    [JsonPropertyName("titulZaJmenem")]
    public string? TitulZaJmenem { get; set; }

    [JsonPropertyName("datumNarozeni")]
    public string? DatumNarozeni { get; set; }

    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("statniObcanstvi")]
    public string? StatniObcanstvi { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }
}

public class InsolvencniRizeni
{
    [JsonPropertyName("datumZapisu")]
    public string? DatumZapisu { get; set; }

    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }
}

public class OboryCinnosti
{
    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("oborNazev")]
    public string? OborNazev { get; set; }
}

public class OdpovedniZastupci
{
    [JsonPropertyName("jmeno")]
    public string? Jmeno { get; set; }

    [JsonPropertyName("prijmeni")]
    public string? Prijmeni { get; set; }

    [JsonPropertyName("titulPredJmenem")]
    public string? TitulPredJmenem { get; set; }

    [JsonPropertyName("titulZaJmenem")]
    public string? TitulZaJmenem { get; set; }

    [JsonPropertyName("datumNarozeni")]
    public string? DatumNarozeni { get; set; }

    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("statniObcanstvi")]
    public string? StatniObcanstvi { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }
}

public class OsobaPodnikatel
{
    [JsonPropertyName("jmeno")]
    public string? Jmeno { get; set; }

    [JsonPropertyName("prijmeni")]
    public string? Prijmeni { get; set; }

    [JsonPropertyName("titulPredJmenem")]
    public string? TitulPredJmenem { get; set; }

    [JsonPropertyName("titulZaJmenem")]
    public string? TitulZaJmenem { get; set; }

    [JsonPropertyName("datumNarozeni")]
    public string? DatumNarozeni { get; set; }

    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("statniObcanstvi")]
    public string? StatniObcanstvi { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }
}

public class OsobyZastupce
{
    [JsonPropertyName("jmeno")]
    public string? Jmeno { get; set; }

    [JsonPropertyName("prijmeni")]
    public string? Prijmeni { get; set; }

    [JsonPropertyName("titulPredJmenem")]
    public string? TitulPredJmenem { get; set; }

    [JsonPropertyName("titulZaJmenem")]
    public string? TitulZaJmenem { get; set; }

    [JsonPropertyName("datumNarozeni")]
    public string? DatumNarozeni { get; set; }

    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("statniObcanstvi")]
    public string? StatniObcanstvi { get; set; }

    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }
}

public class PodminkyProvozovaniZivnosti
{
    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("podminka")]
    public string? Podminka { get; set; }
}

public class PozastaveniProvozovny
{
    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("rozsahPozastaveni")]
    public string? RozsahPozastaveni { get; set; }
}

public class PozastaveniZivnosti
{
    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("rozsahPozastaveni")]
    public string? RozsahPozastaveni { get; set; }
}

public class PreruseniZivnosti
{
    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }
}

public class Provozovny
{
    [JsonPropertyName("sidloProvozovny")]
    public SidloProvozovny? SidloProvozovny { get; set; }

    [JsonPropertyName("umisteniProvozovny")]
    public string? UmisteniProvozovny { get; set; }

    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("pozastaveniProvozovny")]
    public List<PozastaveniProvozovny>? PozastaveniProvozovny { get; set; }

    [JsonPropertyName("typProvozovny")]
    public string? TypProvozovny { get; set; }

    [JsonPropertyName("nazev")]
    public string? Nazev { get; set; }

    [JsonPropertyName("oboryCinnosti")]
    public List<OboryCinnosti>? OboryCinnosti { get; set; }
}

public class ProvozovnyStav
{
    [JsonPropertyName("pocetCelkem")]
    public int? PocetCelkem { get; set; }

    [JsonPropertyName("pocetZaniklych")]
    public int? PocetZaniklych { get; set; }

    [JsonPropertyName("pocetAktivnich")]
    public int? PocetAktivnich { get; set; }

    [JsonPropertyName("pocetPozastavenych")]
    public int? PocetPozastavenych { get; set; }
}

public class Sidlo
{
    [JsonPropertyName("kodStatu")]
    public string? KodStatu { get; set; }

    [JsonPropertyName("nazevStatu")]
    public string? NazevStatu { get; set; }

    [JsonPropertyName("kodKraje")]
    public int? KodKraje { get; set; }

    [JsonPropertyName("nazevKraje")]
    public string? NazevKraje { get; set; }

    [JsonPropertyName("kodOkresu")]
    public int? KodOkresu { get; set; }

    [JsonPropertyName("nazevOkresu")]
    public string? NazevOkresu { get; set; }

    [JsonPropertyName("kodObce")]
    public int? KodObce { get; set; }

    [JsonPropertyName("nazevObce")]
    public string? NazevObce { get; set; }

    [JsonPropertyName("kodSpravnihoObvodu")]
    public int? KodSpravnihoObvodu { get; set; }

    [JsonPropertyName("nazevSpravnihoObvodu")]
    public string? NazevSpravnihoObvodu { get; set; }

    [JsonPropertyName("kodMestskehoObvodu")]
    public int? KodMestskehoObvodu { get; set; }

    [JsonPropertyName("nazevMestskehoObvodu")]
    public string? NazevMestskehoObvodu { get; set; }

    [JsonPropertyName("kodMestskeCastiObvodu")]
    public int? KodMestskeCastiObvodu { get; set; }

    [JsonPropertyName("kodUlice")]
    public int? KodUlice { get; set; }

    [JsonPropertyName("nazevMestskeCastiObvodu")]
    public string? NazevMestskeCastiObvodu { get; set; }

    [JsonPropertyName("nazevUlice")]
    public string? NazevUlice { get; set; }

    [JsonPropertyName("cisloDomovni")]
    public int? CisloDomovni { get; set; }

    [JsonPropertyName("doplnekAdresy")]
    public string? DoplnekAdresy { get; set; }

    [JsonPropertyName("kodCastiObce")]
    public int? KodCastiObce { get; set; }

    [JsonPropertyName("cisloOrientacni")]
    public int? CisloOrientacni { get; set; }

    [JsonPropertyName("cisloOrientacniPismeno")]
    public string? CisloOrientacniPismeno { get; set; }

    [JsonPropertyName("nazevCastiObce")]
    public string? NazevCastiObce { get; set; }

    [JsonPropertyName("kodAdresnihoMista")]
    public int? KodAdresnihoMista { get; set; }

    [JsonPropertyName("psc")]
    public int? Psc { get; set; }

    [JsonPropertyName("textovaAdresa")]
    public string? TextovaAdresa { get; set; }

    [JsonPropertyName("cisloDoAdresy")]
    public string? CisloDoAdresy { get; set; }

    [JsonPropertyName("typCisloDomovni")]
    public string? TypCisloDomovni { get; set; }

    [JsonPropertyName("standardizaceAdresy")]
    public bool? StandardizaceAdresy { get; set; }

    [JsonPropertyName("pscTxt")]
    public string? PscTxt { get; set; }

    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }
}

public class SidloProvozovny
{
    [JsonPropertyName("kodStatu")]
    public string? KodStatu { get; set; }

    [JsonPropertyName("nazevStatu")]
    public string? NazevStatu { get; set; }

    [JsonPropertyName("kodKraje")]
    public int? KodKraje { get; set; }

    [JsonPropertyName("nazevKraje")]
    public string? NazevKraje { get; set; }

    [JsonPropertyName("kodOkresu")]
    public int? KodOkresu { get; set; }

    [JsonPropertyName("nazevOkresu")]
    public string? NazevOkresu { get; set; }

    [JsonPropertyName("kodObce")]
    public int? KodObce { get; set; }

    [JsonPropertyName("nazevObce")]
    public string? NazevObce { get; set; }

    [JsonPropertyName("kodSpravnihoObvodu")]
    public int? KodSpravnihoObvodu { get; set; }

    [JsonPropertyName("nazevSpravnihoObvodu")]
    public string? NazevSpravnihoObvodu { get; set; }

    [JsonPropertyName("kodMestskehoObvodu")]
    public int? KodMestskehoObvodu { get; set; }

    [JsonPropertyName("nazevMestskehoObvodu")]
    public string? NazevMestskehoObvodu { get; set; }

    [JsonPropertyName("kodMestskeCastiObvodu")]
    public int? KodMestskeCastiObvodu { get; set; }

    [JsonPropertyName("kodUlice")]
    public int? KodUlice { get; set; }

    [JsonPropertyName("nazevMestskeCastiObvodu")]
    public string? NazevMestskeCastiObvodu { get; set; }

    [JsonPropertyName("nazevUlice")]
    public string? NazevUlice { get; set; }

    [JsonPropertyName("cisloDomovni")]
    public int? CisloDomovni { get; set; }

    [JsonPropertyName("doplnekAdresy")]
    public string? DoplnekAdresy { get; set; }

    [JsonPropertyName("kodCastiObce")]
    public int? KodCastiObce { get; set; }

    [JsonPropertyName("cisloOrientacni")]
    public int? CisloOrientacni { get; set; }

    [JsonPropertyName("cisloOrientacniPismeno")]
    public string? CisloOrientacniPismeno { get; set; }

    [JsonPropertyName("nazevCastiObce")]
    public string? NazevCastiObce { get; set; }

    [JsonPropertyName("kodAdresnihoMista")]
    public int? KodAdresnihoMista { get; set; }

    [JsonPropertyName("psc")]
    public int? Psc { get; set; }

    [JsonPropertyName("textovaAdresa")]
    public string? TextovaAdresa { get; set; }

    [JsonPropertyName("cisloDoAdresy")]
    public string? CisloDoAdresy { get; set; }

    [JsonPropertyName("typCisloDomovni")]
    public string? TypCisloDomovni { get; set; }

    [JsonPropertyName("standardizaceAdresy")]
    public bool? StandardizaceAdresy { get; set; }

    [JsonPropertyName("pscTxt")]
    public string? PscTxt { get; set; }
}

public class SouvisejiciSubjekty
{
    [JsonPropertyName("typAngazma")]
    public string? TypAngazma { get; set; }

    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("ico")]
    public string? Ico { get; set; }

    [JsonPropertyName("obchodniJmeno")]
    public string? ObchodniJmeno { get; set; }

    [JsonPropertyName("sidlo")]
    public Sidlo? Sidlo { get; set; }

    [JsonPropertyName("typSubjektu")]
    public string? TypSubjektu { get; set; }

    [JsonPropertyName("pravniForma")]
    public string? PravniForma { get; set; }

    [JsonPropertyName("kodStatu")]
    public string? KodStatu { get; set; }

    [JsonPropertyName("osobyZastupce")]
    public List<OsobyZastupce>? OsobyZastupce { get; set; }
}

public class Zaznamy
{
    [JsonPropertyName("ico")]
    public string? Ico { get; set; }

    [JsonPropertyName("obchodniJmeno")]
    public string? ObchodniJmeno { get; set; }

    [JsonPropertyName("sidlo")]
    public Sidlo? Sidlo { get; set; }

    [JsonPropertyName("pravniForma")]
    public string? PravniForma { get; set; }

    [JsonPropertyName("financniUrad")]
    public string? FinancniUrad { get; set; }

    [JsonPropertyName("datumVzniku")]
    public string? DatumVzniku { get; set; }

    [JsonPropertyName("datumZaniku")]
    public string? DatumZaniku { get; set; }

    [JsonPropertyName("datumAktualizace")]
    public string? DatumAktualizace { get; set; }

    [JsonPropertyName("dic")]
    public string? Dic { get; set; }

    [JsonPropertyName("insolvencniRizeni")]
    public InsolvencniRizeni? InsolvencniRizeni { get; set; }

    [JsonPropertyName("datumDoruceniVypisu")]
    public string? DatumDoruceniVypisu { get; set; }

    [JsonPropertyName("adresySubjektu")]
    public List<AdresySubjektu>? AdresySubjektu { get; set; }

    [JsonPropertyName("typSubjektu")]
    public string? TypSubjektu { get; set; }

    [JsonPropertyName("zivnostenskyUrad")]
    public string? ZivnostenskyUrad { get; set; }

    [JsonPropertyName("organizacniSlozka")]
    public string? OrganizacniSlozka { get; set; }

    [JsonPropertyName("zivnostiStav")]
    public ZivnostiStav? ZivnostiStav { get; set; }

    [JsonPropertyName("datumZapisuVr")]
    public string? DatumZapisuVr { get; set; }

    [JsonPropertyName("provozovnyStav")]
    public ProvozovnyStav? ProvozovnyStav { get; set; }

    [JsonPropertyName("primarniZaznam")]
    public bool? PrimarniZaznam { get; set; }

    [JsonPropertyName("kodStatu")]
    public string? KodStatu { get; set; }

    [JsonPropertyName("souvisejiciSubjekty")]
    public List<SouvisejiciSubjekty>? SouvisejiciSubjekty { get; set; }

    [JsonPropertyName("osobaPodnikatel")]
    public OsobaPodnikatel? OsobaPodnikatel { get; set; }

    [JsonPropertyName("angazovaneOsoby")]
    public List<AngazovaneOsoby>? AngazovaneOsoby { get; set; }

    [JsonPropertyName("zivnosti")]
    public List<Zivnosti>? Zivnosti { get; set; }
}

public class ZivnostBezOz
{
    [JsonPropertyName("platnostOd")]
    public string? PlatnostOd { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }
}

public class Zivnosti
{
    [JsonPropertyName("datumVzniku")]
    public string? DatumVzniku { get; set; }

    [JsonPropertyName("datumZaniku")]
    public string? DatumZaniku { get; set; }

    [JsonPropertyName("platnostDo")]
    public string? PlatnostDo { get; set; }

    [JsonPropertyName("predmetPodnikani")]
    public string? PredmetPodnikani { get; set; }

    [JsonPropertyName("pozastaveniZivnosti")]
    public List<PozastaveniZivnosti>? PozastaveniZivnosti { get; set; }

    [JsonPropertyName("podminkyProvozovaniZivnosti")]
    public List<PodminkyProvozovaniZivnosti>? PodminkyProvozovaniZivnosti { get; set; }

    [JsonPropertyName("preruseniZivnosti")]
    public List<PreruseniZivnosti>? PreruseniZivnosti { get; set; }

    [JsonPropertyName("zivnostBezOz")]
    public List<ZivnostBezOz>? ZivnostBezOz { get; set; }

    [JsonPropertyName("datumAktualizace")]
    public string? DatumAktualizace { get; set; }

    [JsonPropertyName("druhZivnosti")]
    public string? DruhZivnosti { get; set; }

    [JsonPropertyName("oboryCinnosti")]
    public List<OboryCinnosti>? OboryCinnosti { get; set; }

    [JsonPropertyName("odpovedniZastupci")]
    public List<OdpovedniZastupci>? OdpovedniZastupci { get; set; }

    [JsonPropertyName("provozovny")]
    public List<Provozovny>? Provozovny { get; set; }
}

public class ZivnostiStav
{
    [JsonPropertyName("pocetAktivnich")]
    public int? PocetAktivnich { get; set; }

    [JsonPropertyName("pocetZaniklych")]
    public int? PocetZaniklych { get; set; }

    [JsonPropertyName("pocetPozastavenych")]
    public int? PocetPozastavenych { get; set; }

    [JsonPropertyName("pocetPrerusenych")]
    public int? PocetPrerusenych { get; set; }

    [JsonPropertyName("pocetCelkem")]
    public int? PocetCelkem { get; set; }
}

