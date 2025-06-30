using Volo.Abp.Modularity;

namespace HledacFirmy;

/* Inherit from this class for your domain layer tests. */
public abstract class HledacFirmyDomainTestBase<TStartupModule> : HledacFirmyTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
