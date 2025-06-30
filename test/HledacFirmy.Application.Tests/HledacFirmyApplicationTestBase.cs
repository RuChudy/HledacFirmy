using Volo.Abp.Modularity;

namespace HledacFirmy;

public abstract class HledacFirmyApplicationTestBase<TStartupModule> : HledacFirmyTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
