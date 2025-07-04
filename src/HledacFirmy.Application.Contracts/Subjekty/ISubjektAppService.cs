using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HledacFirmy.Subjekty
{
    public interface ISubjektAppService : ICrudAppService<SubjektDto, Guid, PagedAndSortedResultRequestDto>
    {
    }
}
