using Microsoft.Extensions.Localization;
using HledacFirmy.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace HledacFirmy.Blazor;

[Dependency(ReplaceServices = true)]
public class HledacFirmyBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<HledacFirmyResource> _localizer;

    public HledacFirmyBrandingProvider(IStringLocalizer<HledacFirmyResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
