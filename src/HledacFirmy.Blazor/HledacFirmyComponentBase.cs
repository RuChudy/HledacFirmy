using HledacFirmy.Localization;
using Volo.Abp.AspNetCore.Components;

namespace HledacFirmy.Blazor;

public abstract class HledacFirmyComponentBase : AbpComponentBase
{
    protected HledacFirmyComponentBase()
    {
        LocalizationResource = typeof(HledacFirmyResource);
    }
}
