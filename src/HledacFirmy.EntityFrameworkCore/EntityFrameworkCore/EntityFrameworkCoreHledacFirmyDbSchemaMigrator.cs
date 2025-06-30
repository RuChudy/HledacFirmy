using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HledacFirmy.Data;
using Volo.Abp.DependencyInjection;

namespace HledacFirmy.EntityFrameworkCore;

public class EntityFrameworkCoreHledacFirmyDbSchemaMigrator
    : IHledacFirmyDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreHledacFirmyDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the HledacFirmyDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<HledacFirmyDbContext>()
            .Database
            .MigrateAsync();
    }
}
