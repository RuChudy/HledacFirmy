using Volo.Abp.Modularity;

namespace HledacFirmy;

[DependsOn(
    typeof(HledacFirmyApplicationModule),
    typeof(HledacFirmyDomainTestModule)
)]
public class HledacFirmyApplicationTestModule : AbpModule
{

}
