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
        var rssSite = new RssSite { Uri = "https://www.ceskenoviny.cz/sluzby/rss/sport.php" };

        int? siteId = await repos.GetSiteIdAsync(rssSite);
        Assert.IsNotNull(siteId);

        if (siteId is not null)
        {
            Feed? feed = await repos.GetByIdAsync(siteId.Value);
            Assert.IsNotNull(feed);

            Assert.IsTrue((await repos.RemoveAsync(siteId.Value)) > 0);
            Assert.IsTrue((await repos.GetSiteIdAsync(rssSite)) is null);

            Assert.IsTrue((await repos.AddOrUpdateAsync(rssSite, feed) > 0));

            siteId = await repos.GetSiteIdAsync(rssSite);
            Assert.IsNotNull(siteId);

            Assert.IsTrue(await repos.DeleteAsync(siteId.Value));
            Assert.IsTrue((await repos.GetSiteIdAsync(rssSite)) is null);

            Assert.IsTrue((await repos.AddOrUpdateAsync(rssSite, feed) > 0));
            Assert.IsTrue((await repos.GetSiteIdAsync(rssSite)) is not null);
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

        var rssSite = new RssSite { Uri = "https://www.ceskenoviny.cz/sluzby/rss/magazin.php" };
        Feed? feed = await rssReader.GetFeedsAsync(rssSite);
        Assert.IsNotNull(feed);
        Assert.IsTrue(feed.Items.Any());

        var repos = _host.Services.GetRequiredService<IRssRepositoryService>();
        Assert.IsInstanceOfType<RssRepositoryService>(repos);

        await repos.AddOrUpdateAsync(rssSite, feed);
    }
}