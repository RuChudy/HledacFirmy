using System;
using HledacFirmy.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HledacFirmy.Subjekty
{
    public class SubjektAppService :
        CrudAppService<Subjekt, SubjektDto, Guid, PagedAndSortedResultRequestDto>,
        ISubjektAppService
    {
        public SubjektAppService(IRepository<Subjekt, Guid> repository)
                : base(repository)
        {

        }
    }
}
