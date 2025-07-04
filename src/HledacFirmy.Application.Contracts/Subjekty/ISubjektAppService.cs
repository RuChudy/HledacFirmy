using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HledacFirmy.Subjekty;

/// <summary>
/// Predpis aplikacni sluzby pro praci se Subjekt.
/// </summary>
public interface ISubjektAppService : ICrudAppService<SubjektDto, Guid, PagedAndSortedResultRequestDto>
{
}
