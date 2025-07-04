using AutoMapper;
using HledacFirmy.Entities;
using HledacFirmy.Subjekty;

namespace HledacFirmy;

public class HledacFirmyApplicationAutoMapperProfile : Profile
{
    public HledacFirmyApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Subjekt, SubjektDto>();
    }
}
