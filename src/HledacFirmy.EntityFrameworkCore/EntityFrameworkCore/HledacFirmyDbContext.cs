using System;
using HledacFirmy.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace HledacFirmy.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class HledacFirmyDbContext :
    AbpDbContext<HledacFirmyDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Subjekt> Subjekty { get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public HledacFirmyDbContext(DbContextOptions<HledacFirmyDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();
        
        /* Configure your own tables/entities inside here */

        builder.Entity<Subjekt>(b =>
        {
            b.ToTable(HledacFirmyConsts.DbTablePrefix + nameof(Subjekt), HledacFirmyConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.HasKey(x => x.Id);
            b.Property(p => p.Ico).IsRequired().HasColumnName(nameof(Subjekt.Ico)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.IcoLength).IsUnicode(true);
            b.Property(p => p.ObchJmeno).IsRequired().HasColumnName(nameof(Subjekt.ObchJmeno)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.ObchJmenoLength).IsUnicode(true);
            b.Property(p => p.Dic).HasColumnName(nameof(Subjekt.Dic)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.DicLength).IsUnicode(true);

            b.Property(p => p.DatumAktualizace).IsRequired().HasColumnName(nameof(Subjekt.DatumAktualizace)).HasColumnType("datetime2");
            b.Property(p => p.DatumVzniku).IsRequired().HasColumnName(nameof(Subjekt.DatumVzniku)).HasColumnType("date");
            b.Property(p => p.DatumZaniku).HasColumnName(nameof(Subjekt.DatumZaniku)).HasColumnType("date");

            b.Property(p => p.Stat).HasColumnName(nameof(Subjekt.Stat)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.BasicTextLength).IsUnicode(true);
            b.Property(p => p.Kraj).HasColumnName(nameof(Subjekt.Kraj)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.BasicTextLength).IsUnicode(true);
            b.Property(p => p.Okres).HasColumnName(nameof(Subjekt.Okres)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.BasicTextLength).IsUnicode(true);
            b.Property(p => p.Obec).HasColumnName(nameof(Subjekt.Obec)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.BasicTextLength).IsUnicode(true);
            b.Property(p => p.Obvod).HasColumnName(nameof(Subjekt.Obvod)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.BasicTextLength).IsUnicode(true);
            b.Property(p => p.Ulice).HasColumnName(nameof(Subjekt.Ulice)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.BasicTextLength).IsUnicode(true);
            b.Property(p => p.CisloDomovni).HasColumnName(nameof(Subjekt.CisloDomovni)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.BasicTextLength).IsUnicode(true);
            b.Property(p => p.CisloOrientacni).HasColumnName(nameof(Subjekt.CisloOrientacni)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.BasicTextLength).IsUnicode(true);
            b.Property(p => p.Psc).HasColumnName(nameof(Subjekt.Psc)).HasColumnType("nvarchar").HasMaxLength(HledacFirmyConsts.PscLength).IsUnicode(true);
            b.Property(p => p.DorucovaciAdresa1).HasColumnName(nameof(Subjekt.DorucovaciAdresa1)).HasColumnType("nvarchar").HasMaxLength(-1).IsUnicode(true);
            b.Property(p => p.DorucovaciAdresa2).HasColumnName(nameof(Subjekt.DorucovaciAdresa2)).HasColumnType("nvarchar").HasMaxLength(-1).IsUnicode(true);
            b.Property(p => p.DorucovaciAdresa3).HasColumnName(nameof(Subjekt.DorucovaciAdresa3)).HasColumnType("nvarchar").HasMaxLength(-1).IsUnicode(true);
            b.Property(p => p.Description).HasColumnName(nameof(Subjekt.Description)).HasColumnType("nvarchar").HasMaxLength(-1).IsUnicode(true);
        });
    }
}
