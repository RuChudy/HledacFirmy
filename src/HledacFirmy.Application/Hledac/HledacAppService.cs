using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HledacFirmy.Ares;
using HledacFirmy.Ares.Services;
using HledacFirmy.Entities;
using HledacFirmy.Subjekty;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HledacFirmy.Hledac;

public class HledacAppService(
    AresHttpClient AresClient,
    IRepository<Subjekt, Guid> SubjektRepo)
    : ApplicationService, IHledacAppService
{
    public async Task<HledacVysledekDto> PostFindAndUpdateIc(long ico)
    {
        Logger.LogDebug($"Try search ico {ico}..");
        try
        {
            if (ico < HledacFirmyConsts.IcoMin || ico > HledacFirmyConsts.IcoMax)
                throw new ArgumentOutOfRangeException(nameof(ico), ico, "Czech ico is out of range.");

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
            subjekt = await PostFirmaDoDatabaze(data);
            ArgumentNullException.ThrowIfNull(subjekt);

            return new HledacVysledekDto { Vysledek = HledacVysledekEnum.Nalezeno, Subjekt = subjekt };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while searching for company ID.");
            return new HledacVysledekDto { Vysledek = HledacVysledekEnum.Chyba, Subjekt = null };
        }
    }

    public async Task<SubjektDto?> GetNalezenyDbSubjektByIc(string ico, DateTime MinDatAktualizace)
    {
        var queryable = await SubjektRepo.GetQueryableAsync();
        var subjekt = await AsyncExecuter.SingleOrDefaultAsync(queryable.Where(s => s.Ico == ico && s.DatumAktualizace >= MinDatAktualizace));

        if (subjekt == null)
            return null;

        return ObjectMapper.Map<Subjekt, SubjektDto>(subjekt);
    }

    public async Task<SubjektDto> PostFirmaDoDatabaze(AresEkonomickySubjekt aresSubjekt)
    {
        if (aresSubjekt == null || aresSubjekt.Ico == null || aresSubjekt.ObchodniJmeno == null)
            throw new ArgumentNullException(nameof(aresSubjekt));

        try
        {
            var queryable = (await SubjektRepo.GetQueryableAsync()).Where(s => s.Ico == aresSubjekt.Ico);
            
            Subjekt? subjekt = await AsyncExecuter.SingleOrDefaultAsync(queryable);
            if (subjekt == null)
            {
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
                return ObjectMapper.Map<Subjekt, SubjektDto>(dbRow);
            }
            else
            {
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
                return ObjectMapper.Map<Subjekt, SubjektDto>(dbRow);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("", ex);
        }
    }
}
