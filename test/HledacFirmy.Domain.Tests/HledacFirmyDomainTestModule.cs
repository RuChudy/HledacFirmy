using Volo.Abp.Modularity;

namespace HledacFirmy;

[DependsOn(
    typeof(HledacFirmyDomainModule),
    typeof(HledacFirmyTestBaseModule)
)]
public class HledacFirmyDomainTestModule : AbpModule
{

}
