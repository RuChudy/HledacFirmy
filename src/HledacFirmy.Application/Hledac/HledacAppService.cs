using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HledacFirmy.Entities;
using HledacFirmy.Subjekty;
using HledacFirmy.Ares;
using HledacFirmy.Ares.Services;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HledacFirmy.Hledac;

/// <summary>
/// Aplikacni sluzba pro hledani ICO firmy. Pokud hledane ICO není v lokalni DB vyhleda v systemu ARES.
/// </summary>
/// <param name="AresClient">Klient ARES sluzby.</param>
/// <param name="SubjektRepo">Repozitar subjektu.</param>
public class HledacAppService(
    AresHttpClient AresClient,
    IRepository<Subjekt, Guid> SubjektRepo)
    : ApplicationService, IHledacAppService
{
    /// <summary>
    /// Vyhleda ICO, v pripade noveho ICO nebo stari zaznamu vice jak 90 dnu, aktualizuje databazi.
    /// </summary>
    /// <param name="ico">IČ hledané firmy.</param>
    /// <returns>Nalezena firma v <see cref="SubjektDto">SubjektDto</see>.</returns>
    public async Task<HledacVysledekDto> PostFindAndUpdateIc(long ico)
    {
        Logger.LogDebug($"Try search ico {ico}..");
        try
        {
            if (ico < HledacFirmyConsts.IcoMin || ico > HledacFirmyConsts.IcoMax)
                throw new ArgumentOutOfRangeException(nameof(ico), ico, "Czech ico is out of range.");

            // Presny format ico pro hledani
            string searchIco = ico.ToString(CultureInfo.InvariantCulture).PadLeft(8, '0');

            // Mame ulozeno v db ?
            SubjektDto? subjekt = await GetNalezenyDbSubjektByIc(searchIco, Clock.Now.Date.AddDays(-90));
            if (subjekt is not null)
            {
                Logger.LogDebug($"Found locally id={subjekt.Id}.");
                return new HledacVysledekDto { Vysledek = HledacVysledekEnum.Nalezeno, Subjekt = subjekt };
            }

            // Najdeme subjekt v ARES
            var data = await AresClient.NactiEkonomickySubjektAsync(searchIco);
            if (data is null)
            {
                Logger.LogInformation($"ARES did not find ico {ico}.");
                return new HledacVysledekDto { Vysledek = HledacVysledekEnum.Nenalezeno, Subjekt = null };
            }

            // Ulozime do databaze
            Logger.LogDebug($"ARES found ico={ico} for '{data.ObchodniJmeno}'. Try update database.");

            subjekt = await PostFirmaDoDatabaze(data);
            ArgumentNullException.ThrowIfNull(subjekt);

            Logger.LogDebug($"Database update id={subjekt.Id}.");

            // Vratime vysledek
            return new HledacVysledekDto { Vysledek = HledacVysledekEnum.Nalezeno, Subjekt = subjekt };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"Error while searching for company ico=´{ico}'.");
            return new HledacVysledekDto { Vysledek = HledacVysledekEnum.Chyba, Subjekt = null };
        }
    }

    /// <summary>
    /// Vyhledá firmu v lokální databázi.
    /// </summary>
    /// <param name="ico">Ico uložené firmy.</param>
    /// <param name="MinDatAktualizace">Datum minimální aktualizace záznamu.</param>
    /// <returns>Uložená firma nebo null.</returns>
    public async Task<SubjektDto?> GetNalezenyDbSubjektByIc(string ico, DateTime MinDatAktualizace)
    {
        var queryable = await SubjektRepo.GetQueryableAsync();

        var subjekt = await AsyncExecuter.SingleOrDefaultAsync(queryable.Where(s => s.Ico == ico && s.DatumAktualizace >= MinDatAktualizace));
        if (subjekt == null)
            return null;

        return ObjectMapper.Map<Subjekt, SubjektDto>(subjekt);
    }

    /// <summary>
    /// Aktualizuje databázi o nalezený ARES subjekt.
    /// </summary>
    /// <param name="aresSubjekt">Nalezený ARES subjekt.</param>
    /// <returns>Uložená firma v lokání databázi.</returns>
    protected async Task<SubjektDto> PostFirmaDoDatabaze(AresEkonomickySubjekt aresSubjekt)
    {
        Logger.LogDebug($"Try update database for subject '{aresSubjekt?.ObchodniJmeno}'..");
        try
        {
            if (aresSubjekt == null || aresSubjekt.Ico == null || aresSubjekt.ObchodniJmeno == null)
                throw new ArgumentNullException(nameof(aresSubjekt));

            var queryable = (await SubjektRepo.GetQueryableAsync()).Where(s => s.Ico == aresSubjekt.Ico);
            
            Subjekt? subjekt = await AsyncExecuter.SingleOrDefaultAsync(queryable);
            if (subjekt == null)
            {
                // Neni v dab zalozime novy
                subjekt = new Subjekt
                {
                    Ico = aresSubjekt.Ico,
                    ObchJmeno = aresSubjekt.ObchodniJmeno,
                    Dic = aresSubjekt.Dic,
                    Stat = aresSubjekt.Sidlo?.NazevStatu,
                    Kraj = aresSubjekt.Sidlo?.NazevKraje,
                    Okres = aresSubjekt.Sidlo?.NazevOkresu,
                    Obec = aresSubjekt.Sidlo?.NazevObce,
                    Obvod = aresSubjekt.Sidlo?.NazevMestskeCastiObvodu,
                    Ulice = aresSubjekt.Sidlo?.NazevUlice,
                    CisloDomovni = aresSubjekt.Sidlo?.CisloDomovni.ToString(),
                    CisloOrientacni = aresSubjekt.Sidlo?.CisloOrientacni.ToString(),
                    Psc = aresSubjekt.Sidlo?.PscTxt,
                    DorucovaciAdresa1 = aresSubjekt.AdresaDorucovaci?.RadekAdresy1,
                    DorucovaciAdresa2 = aresSubjekt.AdresaDorucovaci?.RadekAdresy2,
                    DorucovaciAdresa3 = aresSubjekt.AdresaDorucovaci?.RadekAdresy3,
                    DatumVzniku = aresSubjekt.DatumVzniku ?? new DateTime(1900, 1, 1),
                    DatumZaniku = aresSubjekt.DatumZaniku,
                    DatumAktualizace = Clock.Now,
                };

                var dbRow = await SubjektRepo.InsertAsync(subjekt, true);
                Logger.LogDebug($"Subject inserted Id='{dbRow.Id}'.");

                return ObjectMapper.Map<Subjekt, SubjektDto>(dbRow);
            }
            else
            {
                // Aktualizujeme data
                subjekt.ObchJmeno = aresSubjekt.ObchodniJmeno;
                subjekt.Dic = aresSubjekt.Dic;
                subjekt.Stat = aresSubjekt.Sidlo?.NazevStatu;
                subjekt.Kraj = aresSubjekt.Sidlo?.NazevKraje;
                subjekt.Okres = aresSubjekt.Sidlo?.NazevOkresu;
                subjekt.Obec = aresSubjekt.Sidlo?.NazevObce;
                subjekt.Obvod = aresSubjekt.Sidlo?.NazevMestskeCastiObvodu;
                subjekt.Ulice = aresSubjekt.Sidlo?.NazevUlice;
                subjekt.CisloDomovni = aresSubjekt.Sidlo?.CisloDomovni.ToString();
                subjekt.CisloOrientacni = aresSubjekt.Sidlo?.CisloOrientacni.ToString();
                subjekt.Psc = aresSubjekt.Sidlo?.PscTxt;
                subjekt.DorucovaciAdresa1 = aresSubjekt.AdresaDorucovaci?.RadekAdresy1;
                subjekt.DorucovaciAdresa2 = aresSubjekt.AdresaDorucovaci?.RadekAdresy2;
                subjekt.DorucovaciAdresa3 = aresSubjekt.AdresaDorucovaci?.RadekAdresy3;
                subjekt.DatumVzniku = aresSubjekt.DatumVzniku ?? new DateTime(1900, 1, 1);
                subjekt.DatumZaniku = aresSubjekt.DatumZaniku;
                subjekt.DatumAktualizace = Clock.Now;

                var dbRow = await SubjektRepo.UpdateAsync(subjekt, true);
                Logger.LogDebug($"Subject updated Id='{dbRow.Id}'.");

                return ObjectMapper.Map<Subjekt, SubjektDto>(dbRow);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"Error while sawing Subjekt '{aresSubjekt?.ObchodniJmeno}'.");
            throw new Exception("Error while insert or update Subjekt.", ex);
        }
    }
}
