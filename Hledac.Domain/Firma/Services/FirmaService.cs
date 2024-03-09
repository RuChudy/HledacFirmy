using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Hledac.Database.Context;
using Hledac.Domain.Ares.Services;
using Hledac.Domain.Ares;

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

            var aresSubject = await _aresClient.NajdiEkonomickySubjektAsync(hledaneIco);
            if (aresSubject == null)
            {
                _logger.LogDebug($"Ares {hledaneIco} není!");
                return null;
            }

            // Ulozime do db
            _logger.LogDebug($"Databáze přidáme nově ičo={hledaneIco} '{aresSubject.obchodniJmeno}'.");
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
        if (subjekt == null || subjekt.ico == null || subjekt.obchodniJmeno == null)
            return null;

        Subjekt? dbRow = _db.SaveSubjekt(
            subjekt.ico,
            subjekt.dic,
            subjekt.obchodniJmeno,
            subjekt.sidlo?.nazevStatu,
            subjekt.sidlo?.nazevKraje,
            subjekt.sidlo?.nazevOkresu,
            subjekt.sidlo?.nazevObce,
            subjekt.sidlo?.nazevMestskeCastiObvodu,
            subjekt.sidlo?.nazevUlice,
            subjekt.sidlo?.cisloDomovni.ToString(),
            subjekt.sidlo?.cisloOrientacni.ToString(),
            subjekt.sidlo?.pscTxt,
            subjekt.adresaDorucovaci?.radekAdresy1,
            subjekt.adresaDorucovaci?.radekAdresy2,
            subjekt.adresaDorucovaci?.radekAdresy3,
            subjekt.datumVzniku,
            subjekt.datumZaniku,
            subjekt.datumAktualizace,
            null);

        if (dbRow == null)
            return null;

        return dbRow.ToFirmaDto();
    }
}
