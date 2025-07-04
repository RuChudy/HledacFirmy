using System;
using System.Threading.Tasks;
using HledacFirmy.Subjekty;

namespace HledacFirmy.Hledac;

/// <summary>
/// Predpis aplikacni sluzby pro hledani ICO firmy. Pokud hledane ICO není v lokalni DB vyhleda v systemu ARES.
/// </summary>
public interface IHledacAppService
{
    /// <summary>
    /// Vyhleda ICO, v pripade noveho ICO nebo stari zaznamu vice jak 90 dnu, aktualizuje databazi.
    /// </summary>
    /// <param name="ico">ICO hledane firmy.</param>
    /// <returns>Nalezena firma v <see cref="SubjektDto">SubjektDto</see>.</returns>
    Task<HledacVysledekDto> PostFindAndUpdateIc(long ico);

    /// <summary>
    /// Vyhledá firmu v lokální databázi.
    /// </summary>
    Task<SubjektDto?> GetNalezenyDbSubjektByIc(string ico, DateTime MinDatAktualizace);
}
