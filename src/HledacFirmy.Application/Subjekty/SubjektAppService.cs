using System;
using HledacFirmy.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HledacFirmy.Subjekty;

/// <summary>
/// Aplikacni sluzba pro praci se Subjekt.
/// </summary>
/// <param name="repository">Repozitar pro Subjekt.</param>
public class SubjektAppService(IRepository<Subjekt, Guid> repository) :
    CrudAppService<Subjekt, SubjektDto, Guid, PagedAndSortedResultRequestDto>(repository),
    ISubjektAppService
{
}
