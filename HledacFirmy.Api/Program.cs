using Microsoft.EntityFrameworkCore;
using Hledac.Database;
using Hledac.Database.Context;
using Hledac.Domain.Ares.Services;
using Hledac.Domain.Firma.Services;
using Hledac.Domain.Firma;
using Hledac.Domain.Rss.Services;
using Hledac.Domain.Rss;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddDbContext<SubjektDbContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services.Configure<AresSettings>(builder.Configuration.GetSection("Ares"));
builder.Services.AddHttpClient<AresHttpClient>();
builder.Services.AddScoped<FirmaService>();

builder.Services.Configure<RssSettings>(builder.Configuration.GetSection(RssSettings.SectionName));
builder.Services.AddHttpClient<RssHttpClient>();
builder.Services.AddScoped<IRssReaderService, RssReaderService>();
builder.Services.AddScoped<IRssRepositoryService, RssRepositoryService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Test na funkcnost
app.MapGet("/", () => "Hello World!").WithName("HelloWorld").WithTags("health");

// Vyhledávač iča
app.MapPost("/najdi-ico/{ico}", ApiPostNajdiIco).WithName("NajdiIco").WithTags("ico").Produces(404).Produces<FirmaDto>();
app.MapGet("/ico/", UlozenaIca).WithName("UlozenaIca").WithTags("ico").Produces<IList<string>>();

// Rss kanály
app.MapGet("/rss/all", ApiGetAllRssSite).WithName("RssAll").WithTags("rss").Produces<IEnumerable<RssCachedSite>>();
app.MapGet("/rss/feed/{id}", ApiGetRssFeed).WithName("RssFeed").WithTags("rss").Produces(404).Produces<Feed>();
app.MapDelete("/rss/feed/{id}", ApiDeleteRssFeed).WithName("RssDeleteFeed").WithTags("rss").Produces(404);
app.MapPost("/rss/feed", ApiPostAddOrUpdateRssSite).WithName("RssAddFeed").WithTags("rss").Produces<Feed>();
app.MapPost("/rss/delmore", ApiPostDeleteRssFeedBatch).WithName("RssDeleteMoreFeeds").WithTags("rss").Produces(404);

app.Run();

/// <summary>Najde IC v databázi, pokud neni nacte z ARES a uloží.</summary>
static async Task<IResult> ApiPostNajdiIco(string ico, FirmaService fs)
{
    var firma = await fs.NajdiFirmuDleIcoNeboNullAsync(ico);
    return (firma == null) ? TypedResults.NotFound() : TypedResults.Ok(firma);
}

/// <summary>Seznam uložených IC v databázi.</summary>
static async Task<IResult> UlozenaIca(FirmaService fs)
{
    var seznamIc = await fs.UlozenaIca();
    return TypedResults.Ok(seznamIc);
}

/// <summary>Přidá další nebo aktualizuje existující rss kanál.</summary>
static async Task<IResult> ApiPostAddOrUpdateRssSite([FromBody] RssSiteUri rssUri, IRssRepositoryService rssRepository, IRssReaderService rssReader, CancellationToken cancellation)
{
    ArgumentNullException.ThrowIfNull(rssUri?.Uri);

    RssCachedSite? site = await rssRepository.GetSiteAsync(rssUri, cancellation);
    if (site == null)
    {
        Feed? newFeed = await rssReader.GetFeedsAsync(rssUri, cancellation);

        cancellation.ThrowIfCancellationRequested();

        await rssRepository.AddOrUpdateAsync(rssUri, newFeed, cancellation);

        site = await rssRepository.GetSiteAsync(rssUri, cancellation);
    }

    ArgumentNullException.ThrowIfNull(site);

    var result = await rssRepository.GetFeedByIdAsync(site.Id, cancellation);
    return TypedResults.Ok(result);
}

/// <summary>Načte seznam všech rss kanálů.</summary>
static async Task<IResult> ApiGetAllRssSite(IRssRepositoryService rssRepository, CancellationToken cancellation)
{
    var sites = await rssRepository.GetAllSitesAsync(0, short.MaxValue, cancellation);
    return TypedResults.Ok(sites);
}

/// <summary>Načte detail rss kanálu se zprávami.</summary>
static async Task<IResult> ApiGetRssFeed(int id, IRssRepositoryService rssRepository, CancellationToken cancellation)
{
    var feed = await rssRepository.GetFeedByIdAsync(id, cancellation);
    return (feed == null) ? TypedResults.NotFound() : TypedResults.Ok(feed);
}

/// <summary>Označí rss kanál jako smazaný.</summary>
static async Task<IResult> ApiDeleteRssFeed(int id, IRssRepositoryService rssRepository, CancellationToken cancellation)
{
    return (await rssRepository.DeleteAsync(id, cancellation) == true) ? TypedResults.Ok() : TypedResults.NotFound();
}

/// <summary>Označí několik rss kanál jako smazané.</summary>
static async Task<IResult> ApiPostDeleteRssFeedBatch(IEnumerable<int> id, IRssRepositoryService rssRepository, CancellationToken cancellation)
{
    return (await rssRepository.BulkDeleteAsync(id, cancellation) > 0) ? TypedResults.Ok() : TypedResults.NotFound();
}
