
namespace Hledac.Domain.Ares;

public static class Subject
{
    public static IEnumerable<SubjectDbInfo?>? Create(IEnumerable<AresVrEkonomickeSubjekty?>? dbInfos)
    {
        return dbInfos?.Select(Create).AsEnumerable();
    }

    public static SubjectDbInfo? Create(AresVrEkonomickeSubjekty? dbInfo)
    {
        if (dbInfo == null)
            return null;

        if (dbInfo.Zaznamy is null || dbInfo.Zaznamy.Count <= 0)
            return new SubjectDbInfo() { ICO = dbInfo.IcoId, ResponseError = "Nenalezeno" };

        if (dbInfo.Zaznamy.Count == 1)
            return Create(dbInfo.Zaznamy[0]);

        if (dbInfo.Zaznamy.Count(a => a.DatumAktualizace is not null && a.PrimarniZaznam is true) <= 0)
        {
            var convert = Create(dbInfo.Zaznamy[0]);
            convert.ResponseError = "Prvni z mnoha";
            return convert;
        }
        else
        {
            var actual = dbInfo.Zaznamy
                .Where(a => a.DatumAktualizace is not null && a.PrimarniZaznam is true)
                .OrderByDescending(a => a.DatumAktualizace)
                .First();

            return Create(actual);
        }
    }

    public static SubjectDbInfo Create(AresVrZaznamy dbInfo)
    {
        var firma = dbInfo.ObchodniJmeno?.Where(a => a.DatumVymazu is null).OrderByDescending(d => d.DatumZapisu).FirstOrDefault()?.Hodnota;
        var ico = dbInfo.Ico?.Where(a => a.DatumVymazu is null).OrderByDescending(d => d.DatumZapisu).FirstOrDefault()?.Hodnota;
        var spis = dbInfo.SpisovaZnacka?.Where(z => z.DatumVymazu is null).OrderByDescending(d => d.DatumZapisu).FirstOrDefault();
        var adres = dbInfo.Adresy?.Where(a => a.DatumVymazu is null).OrderByDescending(d => d.DatumZapisu).Select(d => d.Adresa).FirstOrDefault();
        var forma = dbInfo.PravniForma?.Where(a => a.DatumVymazu is null).OrderByDescending(d => d.DatumZapisu).FirstOrDefault()?.Hodnota;

        var likvidace = false;
        if (firma is not null)
            likvidace = firma.IndexOf("v likvidaci", StringComparison.CurrentCultureIgnoreCase) > 0;

        return new SubjectDbInfo()
        {
            ICO = ico,
            PravniForma = forma,
            DatumPosledniKontroly = dbInfo.DatumAktualizace,
            Rejstrik = dbInfo.Rejstrik,
            RequestError = null,
            ResponseError = null,
            Aktualizace_DB = DateTime.Now,
            TypVypisu = dbInfo.PrimarniZaznam ? "aktualni" : null,
            S_StavSubjektu = dbInfo.StavSubjektu,
            ObchodniFirma = firma,
            S_Likvidace = likvidace ? 1 : null,
            PF_Kody = forma,
            DatumZapisu = spis?.DatumZapisu,
            MistoZapisu = spis?.Soud,
            ZnackaZapisu = String.Concat(spis?.Oddil, ' ', spis?.Vlozka).Trim(),

            // A_IDAdresy = adres?.
            A_KodStatu = adres?.KodStatu,
            A_NazevStatu = adres?.NazevStatu,
            A_NazevOkresu = adres?.NazevOkresu,
            A_NazevObce = adres?.NazevObce,
            A_NazevCastiObce = adres?.NazevCastiObce,
            A_NazevUlice = adres?.NazevUlice,
            A_CisloDomovni = (int?)adres?.CisloDomovni,
            A_TypCisloDomovni = adres?.TypCisloDomovni,
            A_CisloOrientacni = adres?.CisloOrientacni?.ToString("G"),
            A_PSC = adres?.Psc?.ToString("G"),
        };
    }
}

public class SubjectDbInfo
{
    public string? ICO { get; set; }
    public string? PravniForma { get; set; }
    public DateTime? DatumPosledniKontroly { get; set; }
    public string? Rejstrik { get; set; }
    public string? RequestError { get; set; }
    public string? ResponseError { get; set; }
    public DateTime? Aktualizace_DB { get; set; }
    public string? TypVypisu { get; set; }
    public string? S_StavSubjektu { get; set; }
    public int? S_Konkurz { get; set; }
    public int? S_Vyrovnani { get; set; }
    public int? S_Zamitnuti { get; set; }
    public int? S_Likvidace { get; set; }
    public string? ObchodniFirma { get; set; }
    public string? Jmeno { get; set; }
    public string? Prijmeni { get; set; }
    public DateTime? DatumNarozeni { get; set; }
    public string? PF_Kody { get; set; }
    public string? PF_Nazev { get; set; }
    public string? PF_Osoba { get; set; }
    public string? PF_Text { get; set; }
    public string? A_IDAdresy { get; set; }
    public string? A_KodStatu { get; set; }
    public string? A_NazevStatu { get; set; }
    public string? A_NazevOkresu { get; set; }
    public string? A_NazevObce { get; set; }
    public string? A_NazevCastiObce { get; set; }
    public string? A_NazevUlice { get; set; }
    public int? A_CisloDomovni { get; set; }
    public string? A_TypCisloDomovni { get; set; }
    public string? A_CisloOrientacni { get; set; }
    public string? A_PSC { get; set; }
    public DateTime? DatumZapisu { get; set; }
    public string? MistoZapisu { get; set; }
    public string? ZnackaZapisu { get; set; }
}
