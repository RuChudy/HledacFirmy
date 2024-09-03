using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Hledac.Database.Context;
using Hledac.Domain.Ares.Services;
using Hledac.Domain.Ares;
using Azure;

namespace Hledac.Domain.Firma.Services;

/// <summary>
/// Služba nad firmou (subjektem).
/// </summary>
public class FirmaService
{
    private readonly ILogger<FirmaService> _logger;
    private readonly AresHttpClient _aresClient;
    private readonly SubjektDbContext _db;

    private static readonly Regex icoRegex = new Regex(@"^[0-9]{1,8}$", RegexOptions.Compiled);

    public FirmaService(ILogger<FirmaService> logger, AresHttpClient aresClient, SubjektDbContext db)
    {
        _logger = logger;
        _aresClient = aresClient;
        _db = db;
    }

    /// <summary>
    /// Vrátí seznam uložených ič (max 100).
    /// </summary>
    /// <returns>Seznam ulozenych ic.</returns>
    public async Task<List<string>> UlozenaIca()
    {
        return await _db.Subjekty
            .OrderByDescending(s => s.Created)
            .Take(100)
            .Select(s => s.Ico)
            .ToListAsync();
    }

    /// <summary>
    /// Koukni do databaze, zda tam je ico. Pokud ne, tak zaloz tuto firmu.
    /// </summary>
    /// <param name="ico">Ico fitmy.</param>
    /// <returns></returns>
    public async Task<FirmaDto?> NajdiFirmuDleIcoNeboNullAsync(string? ico)
    {
        if (!JeToIco(ico))
            throw new ArgumentOutOfRangeException(nameof(ico), ico, "Není platné ičo.");

        string hledaneIco = ico?.PadLeft(8, '0') ?? String.Empty;

        // Hledame v databazi, zda je ico
        _logger.LogDebug($"Databáze hledám ičo={hledaneIco}..");

        FirmaDto? subjektDb = await _db.Subjekty
            .Where(s => s.Ico.Equals(hledaneIco) && s.Deleted == null)
            .OrderByDescending(s => s.Created)
            .Select(subjekt => subjekt.ToFirmaDto())
            .SingleOrDefaultAsync();

        if (subjektDb == null)
        {
            // Hledame v ARES
            _logger.LogDebug($"Databáze nemá ičo={hledaneIco}. Koukneme do ARES..");

            var aresSubject = await _aresClient.NactiEkonomickySubjektAsync(hledaneIco);
            if (aresSubject == null)
            {
                _logger.LogDebug($"Ares {hledaneIco} není!");
                return null;
            }

            // Ulozime do db
            _logger.LogDebug($"Databáze přidáme nově ičo={hledaneIco} '{aresSubject.ObchodniJmeno}'.");
            return UlozFirmaDoDatabaze(aresSubject) ?? throw new ArgumentNullException(nameof(FirmaDto));
        }
        else
        {
            _logger.LogDebug($"Database nalezeno ičo='{hledaneIco}' '{subjektDb.Jmeno}'.");
            return subjektDb;
        }
    }

    /// <summary>
    /// Kontrola, zda text je ico.
    /// </summary>
    /// <param name="ico">Ico ke kontrole.</param>
    /// <returns>True, pokud sedí formát ico.</returns>
    private static bool JeToIco(string? ico)
    {
        if (string.IsNullOrWhiteSpace(ico))
            return false;

        if (!icoRegex.IsMatch(ico))
            return false;

        return true;
    }

    /// <summary>
    /// Vytvori zaznam o firme z ares subjektu.
    /// </summary>
    /// <param name="subjekt">Ares subjekt,</param>
    /// <returns>Zaznam o firme.</returns>
    private FirmaDto? UlozFirmaDoDatabaze(AresEkonomickySubjekt subjekt)
    {
        if (subjekt == null || subjekt.Ico == null || subjekt.ObchodniJmeno == null)
            return null;

        Subjekt? dbRow = _db.SaveSubjekt(
            subjekt.Ico,
            subjekt.Dic,
            subjekt.ObchodniJmeno,
            subjekt.Sidlo?.NazevStatu,
            subjekt.Sidlo?.NazevKraje,
            subjekt.Sidlo?.NazevOkresu,
            subjekt.Sidlo?.NazevObce,
            subjekt.Sidlo?.NazevMestskeCastiObvodu,
            subjekt.Sidlo?.NazevUlice,
            subjekt.Sidlo?.CisloDomovni.ToString(),
            subjekt.Sidlo?.CisloOrientacni.ToString(),
            subjekt.Sidlo?.PscTxt,
            subjekt.AdresaDorucovaci?.RadekAdresy1,
            subjekt.AdresaDorucovaci?.RadekAdresy2,
            subjekt.AdresaDorucovaci?.RadekAdresy3,
            subjekt.DatumVzniku,
            subjekt.DatumZaniku,
            subjekt.DatumAktualizace,
            null);

        if (dbRow == null)
            return null;

        return dbRow.ToFirmaDto();
    }

    /// <summary>
    /// </summary>
    public SubjectVr? UlozFirmaVrDoDatabaze(SubjectDbInfo subjekt)
    {
        if (subjekt == null || subjekt.ICO == null || subjekt.ObchodniFirma == null)
            return null;

        SubjectVr? dbRow = _db.UpdateSubjektVr(new SubjectVr
        {
            ICO = subjekt.ICO,
            PravniForma = (subjekt.PravniForma is null) ? null : int.Parse(subjekt.PravniForma.Trim()),
            DatumPosledniKontroly = subjekt.DatumPosledniKontroly,
            Rejstrik = subjekt.Rejstrik,
            RequestError = subjekt.RequestError,
            ResponseError = subjekt.ResponseError,
            Aktualizace_DB = subjekt.Aktualizace_DB,
            TypVypisu = subjekt.TypVypisu,
            S_StavSubjektu = subjekt.S_StavSubjektu,
            S_Konkurz = subjekt.S_Konkurz,
            S_Vyrovnani = subjekt.S_Vyrovnani,
            S_Zamitnuti = subjekt.S_Zamitnuti,
            S_Likvidace = subjekt.S_Likvidace,
            ObchodniFirma = subjekt.ObchodniFirma,
            Jmeno = subjekt.Jmeno,
            Prijmeni = subjekt.Prijmeni,
            DatumNarozeni = subjekt.DatumNarozeni,
            PF_Kody = (subjekt.PF_Kody is null) ? null : int.Parse(subjekt.PF_Kody.Trim()),
            PF_Nazev = subjekt.PF_Nazev,
            PF_Osoba = subjekt.PF_Osoba,
            PF_Text = subjekt.PF_Text,
            A_IDAdresy = subjekt.A_IDAdresy,
            A_KodStatu = subjekt.A_KodStatu,
            A_NazevStatu = subjekt.A_NazevStatu,
            A_NazevOkresu = subjekt.A_NazevOkresu,
            A_NazevObce = subjekt.A_NazevObce,
            A_NazevCastiObce = subjekt.A_NazevCastiObce,
            A_NazevUlice = subjekt.A_NazevUlice,
            A_CisloDomovni = subjekt.A_CisloDomovni,
            A_TypCisloDomovni = (subjekt.A_TypCisloDomovni is null) ? null : int.Parse(subjekt.A_TypCisloDomovni.Trim()),
            A_CisloOrientacni = subjekt.A_CisloOrientacni,
            A_PSC = subjekt.A_PSC,
            DatumZapisu = subjekt.DatumZapisu,
            MistoZapisu = subjekt.MistoZapisu,
            ZnackaZapisu = subjekt.ZnackaZapisu
        });

        return dbRow;
    }

}
