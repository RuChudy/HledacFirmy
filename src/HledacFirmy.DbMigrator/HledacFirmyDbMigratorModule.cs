using HledacFirmy.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace HledacFirmy.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HledacFirmyEntityFrameworkCoreModule),
    typeof(HledacFirmyApplicationContractsModule)
)]
public class HledacFirmyDbMigratorModule : AbpModule
{
}
