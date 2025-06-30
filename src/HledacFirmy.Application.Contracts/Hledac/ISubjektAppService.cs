using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HledacFirmy.Hledac
{
    public interface ISubjektAppService : ICrudAppService<SubjektDto, Guid, PagedAndSortedResultRequestDto>
    {
    }
}
