using HledacFirmy.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HledacFirmy.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HledacFirmyController : AbpControllerBase
{
    protected HledacFirmyController()
    {
        LocalizationResource = typeof(HledacFirmyResource);
    }
}
