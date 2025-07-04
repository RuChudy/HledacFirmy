using System;
using System.Threading.Tasks;
using HledacFirmy.Subjekty;

namespace HledacFirmy.Hledac;

public interface IHledacAppService
{
    Task<HledacVysledekDto> PostFindAndUpdateIc(long ico);
    Task<SubjektDto?> GetNalezenyDbSubjektByIc(string ico, DateTime MinDatAktualizace);
}
