using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HledacFirmy.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class HledacFirmyDbContextFactory : IDesignTimeDbContextFactory<HledacFirmyDbContext>
{
    public HledacFirmyDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        HledacFirmyEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<HledacFirmyDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new HledacFirmyDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../HledacFirmy.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
