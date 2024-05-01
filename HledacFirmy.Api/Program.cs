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
app.MapGet("/", () => "Hello World!").WithName("HelloWorld").WithOpenApi();

// Vyhledávač iča
app.MapPost("/najdi-ico/{ico}", ApiPostNajdiIco).WithName("NajdiIco").Produces(404).Produces<FirmaDto>().WithOpenApi();

// Rss kanály
app.MapGet("/rss/all", ApiGetAllRssSite).WithName("RssAll").Produces<IEnumerable<RssCachedSite>>().WithOpenApi();
app.MapGet("/rss/feed/{id}", ApiGetRssFeed).WithName("RssFeed").Produces(404).Produces<Feed>().WithOpenApi();
app.MapPost("/rss/feed", ApiPostAddOrUpdateRssSite).WithName("RssAddFeed").Produces<Feed>().WithOpenApi();
app.MapDelete("/rss/feed/{id}", ApiDeleteRssFeed).WithName("RssDeleteFeed").Produces(404).WithOpenApi();

app.Run();

static async Task<IResult> ApiPostNajdiIco(string ico, FirmaService fs)
{
    var firma = await fs.NajdiFirmuDleIcoNeboNullAsync(ico);
    return (firma == null) ? TypedResults.NotFound() : TypedResults.Ok(firma);
}

static async Task<IResult> ApiPostAddOrUpdateRssSite([FromBody] RssSiteUri rssUri, IRssRepositoryService rssRepository, IRssReaderService rssReader)
{
    ArgumentNullException.ThrowIfNull(rssUri?.Uri);

    var site = await rssRepository.GetSiteAsync(rssUri);
    if (site == null)
    {
        var newFeed = await rssReader.GetFeedsAsync(rssUri);
        await rssRepository.AddOrUpdateAsync(rssUri, newFeed);

        site = await rssRepository.GetSiteAsync(rssUri);
    }

    ArgumentNullException.ThrowIfNull(site);

    var result = await rssRepository.GetFeedByIdAsync(site.Id);
    return TypedResults.Ok(result);
}

static async Task<IResult> ApiGetAllRssSite(IRssRepositoryService rssRepository)
{
    var sites = await rssRepository.GetAllSitesAsync(0, short.MaxValue);
    return TypedResults.Ok(sites);
}

static async Task<IResult> ApiGetRssFeed(int id, IRssRepositoryService rssRepository)
{
    var feed = await rssRepository.GetFeedByIdAsync(id);
    return (feed == null) ? TypedResults.NotFound() : TypedResults.Ok(feed);
}

static async Task<IResult> ApiDeleteRssFeed(int id, IRssRepositoryService rssRepository)
{
    return (await rssRepository.DeleteAsync(id) == true) ? TypedResults.Ok() : TypedResults.NotFound();
}
