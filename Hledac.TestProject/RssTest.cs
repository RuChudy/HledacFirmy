using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hledac.Database;
using Hledac.Domain.Rss.Services;
using Hledac.Database.Context;
using Hledac.Domain.Rss;

namespace Hledac.TestProject;

[TestClass]
public class RssTest
{
    public required IHost _host;

    [TestInitialize]
    public void TestInitialize() => _host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(configuration =>
        {
            _ = configuration.AddJsonFile("appsettings.json");
        })
        .ConfigureServices((host, services) =>
        {
            _ = services.Configure<ConnectionStrings>(host.Configuration.GetSection("ConnectionStrings"));
            _ = services.Configure<RssSettings>(host.Configuration.GetSection(RssSettings.SectionName));

            _ = services.AddDbContext<SubjektDbContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            _ = services.AddHttpClient<RssHttpClient>();
            _ = services.AddScoped<IRssReaderService, RssReaderService>();
            _ = services.AddScoped<IRssRepositoryService, RssRepositoryService>();

        })
        .Build();

    [TestMethod]
    public async Task RssDbUpdateTest()
    {
        var repos = _host.Services.GetRequiredService<IRssRepositoryService>();
        Assert.IsInstanceOfType<RssRepositoryService>(repos);

        // sport je v databázi stále
        var rssUri = new RssSiteUri { Uri = "https://www.ceskenoviny.cz/sluzby/rss/sport.php" };

        RssCachedSite? site = await repos.GetSiteAsync(rssUri);
        Assert.IsNotNull(site);

        if (site is not null)
        {
            Feed? feed = await repos.GetFeedByIdAsync(site.Id);
            Assert.IsNotNull(feed);

            Assert.IsTrue((await repos.RemoveAsync(site.Id)) > 0);
            Assert.IsTrue((await repos.GetSiteAsync(rssUri)) is null);

            Assert.IsTrue((await repos.AddOrUpdateAsync(rssUri, feed) > 0));

            site = await repos.GetSiteAsync(rssUri);
            Assert.IsNotNull(site);

            Assert.IsTrue(await repos.DeleteAsync(site.Id));
            Assert.IsTrue((await repos.GetSiteAsync(rssUri)) is null);

            Assert.IsTrue((await repos.AddOrUpdateAsync(rssUri, feed) > 0));
            Assert.IsTrue((await repos.GetSiteAsync(rssUri)) is not null);
        }
    }

    [TestMethod]
    public async Task RssReadAndUpdateTest()
    {
        var rssReader = _host.Services.GetRequiredService<IRssReaderService>();
        Assert.IsInstanceOfType<RssReaderService>(rssReader);

        /*
            Vše              https://www.ceskenoviny.cz/sluzby/rss/zpravy.php
            Zprávy z ČR      https://www.ceskenoviny.cz/sluzby/rss/cr.php           https://www.ceskenoviny.cz/
            Zprávy ze světa  https://www.ceskenoviny.cz/sluzby/rss/svet.php
            Ekonomika        https://www.ceskenoviny.cz/sluzby/rss/ekonomika.php
            Kultura          https://www.ceskenoviny.cz/sluzby/rss/kultura.php
            Magazín          https://www.ceskenoviny.cz/sluzby/rss/magazin.php
            Sport            https://www.ceskenoviny.cz/sluzby/rss/sport.php
            Fotbal           https://www.ceskenoviny.cz/sluzby/rss/fotbal.php
            Hokej            https://www.ceskenoviny.cz/sluzby/rss/hokej.php
            Tenis            https://www.ceskenoviny.cz/sluzby/rss/tenis.php
        */

        var rssSite = new RssSiteUri { Uri = "https://www.ceskenoviny.cz/sluzby/rss/magazin.php" };
        Feed? feed = await rssReader.GetFeedsAsync(rssSite);
        Assert.IsNotNull(feed);
        Assert.IsTrue(feed.Items.Any());

        var repos = _host.Services.GetRequiredService<IRssRepositoryService>();
        Assert.IsInstanceOfType<RssRepositoryService>(repos);

        await repos.AddOrUpdateAsync(rssSite, feed);
    }
}