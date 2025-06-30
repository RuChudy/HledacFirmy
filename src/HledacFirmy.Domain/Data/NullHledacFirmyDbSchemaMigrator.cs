using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HledacFirmy.Data;

/* This is used if database provider does't define
 * IHledacFirmyDbSchemaMigrator implementation.
 */
public class NullHledacFirmyDbSchemaMigrator : IHledacFirmyDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
