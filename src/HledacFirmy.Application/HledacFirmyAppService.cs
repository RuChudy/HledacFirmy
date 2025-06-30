using HledacFirmy.Localization;
using Volo.Abp.Application.Services;

namespace HledacFirmy;

/* Inherit your application services from this class.
 */
public abstract class HledacFirmyAppService : ApplicationService
{
    protected HledacFirmyAppService()
    {
        LocalizationResource = typeof(HledacFirmyResource);
    }
}
