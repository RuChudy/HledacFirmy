using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hledac.Database;
using Hledac.Domain.Rss.Services;
using Hledac.Database.Context;

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
    public async Task RssReadTest()
    {
        var rssService = _host.Services.GetRequiredService<IRssReaderService>();
        Assert.IsInstanceOfType<RssReaderService>(rssService);

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

        var feed = await rssService.GetFeedsAsync(rssSite);
        Assert.IsNotNull(feed);
        Assert.IsTrue(feed?.Items.Any());

        /*
        var repos = _host.Services.GetRequiredService<IRssRepositoryService>();
        Assert.IsInstanceOfType<RssRepositoryService>(repos);

        await repos.AddAsync(rssSite, feed);
        */
    }
}