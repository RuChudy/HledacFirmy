global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;

namespace Hledac.Database.Context;

/// <summary>
/// Databáze pro subjekty.
/// </summary>
public partial class SubjektDbContext : DbContext
{
    public SubjektDbContext(DbContextOptions options)
        : base (options)
    {

    }

    public DbSet<Subjekt> Subjekty { get; set; }
    public DbSet<RssCacheFeed> RssCacheFeeds { get; set; }
    public DbSet<RssCacheFeedItem> RssCacheFeedItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subjekt>();
        modelBuilder.Entity<RssCacheFeed>();
        modelBuilder.Entity<RssCacheFeedItem>();
    }
}

/// <summary>
/// Pro EF tools.
/// </summary>
public class SubjektDbContextDesignTimeFactory : IDesignTimeDbContextFactory<SubjektDbContext>
{
    public SubjektDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<SubjektDbContext>();
        builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-sbjkt-43a40cdc-aa5e-4a59-ac3a-df86e3e3683b;Trusted_Connection=True;MultipleActiveResultSets=true");
        return new SubjektDbContext(builder.Options);
    }
}
