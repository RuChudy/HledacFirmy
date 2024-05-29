using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hledac.Database;
using Hledac.Domain.Rss.Services;
using Hledac.Database.Context;
using Hledac.Domain.Ares.Services;
using Hledac.Domain.Firma.Services;
using Hledac.Domain.Ares;
using Hledac.Domain.Rss;

namespace Hledac.TestProject;

[TestClass]
public class AresTest
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
            _ = services.Configure<AresSettings>(host.Configuration.GetSection("Ares"));

            _ = services.AddDbContext<SubjektDbContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            _ = services.AddHttpClient<AresHttpClient>();
            _ = services.AddScoped<FirmaService>();
        })
        .Build();

    [TestMethod]
    public async Task CtReadAresTest()
    {
        var aresClient = _host.Services.GetRequiredService<AresHttpClient>();
        Assert.IsInstanceOfType<AresHttpClient>(aresClient);

        string ceskaTelevizeIco = "00027383"; // ico?.PadLeft(8, '0')
        AresEkonomickySubjekt? result = await aresClient.NactiEkonomickySubjektAsync(ceskaTelevizeIco);
        Assert.IsNotNull(result);

        if (result?.SeznamRegistraci?.InRes() == true)
            Assert.IsTrue(true);

        if (result?.SeznamRegistraci?.InRzp() == true)
        {
            AresRZP? rzp = await aresClient.NactiRZPAsync(result.Ico ?? throw new ArgumentNullException(nameof(result.Ico)));
        }
    }

    [TestMethod]
    public async Task CtReadDbTest()
    {
        var firmaService = _host.Services.GetRequiredService<FirmaService>();
        Assert.IsInstanceOfType<FirmaService>(firmaService);

        int ceskaTelevizeIco = 27_383;
        await firmaService.NajdiFirmuDleIcoNeboNullAsync(ceskaTelevizeIco.ToString());
    }

    [TestMethod]
    public async Task InsolvenceTest()
    {
        var aresClient = _host.Services.GetRequiredService<AresHttpClient>();
        Assert.IsInstanceOfType<AresHttpClient>(aresClient);

        string sroInsolvence = "25291441";
        AresEkonomickySubjekt? result = await aresClient.NactiEkonomickySubjektAsync(sroInsolvence);
        Assert.IsNotNull(result);

        if (result?.SeznamRegistraci?.InVr() == true)
        {
            AresVrEkonomickeSubjekty? test = await aresClient.NactiVrAsync(sroInsolvence);
            Assert.IsNotNull(test);
        }

        if (result?.SeznamRegistraci?.InRes() == true)
        {
            Assert.IsTrue(true);
        }

        if (result?.SeznamRegistraci?.InRzp() == true)
        {
            AresRZP? rzp = await aresClient.NactiRZPAsync(result.Ico ?? throw new ArgumentNullException(nameof(result.Ico)));
        }
    }

}