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

        /*
         1453599

        ID=1478
        ICO=1453599
        PravniForma=2
        DatumPosledniKontroly=2024-01-02 05:14:11.000
        DatumZpracovani=NULL
        Rejstrik=Vypis_OR
        RequestError=NULL
        ResponseError=NULL
        Aktualizace_DB=2024-01-01 00:00:00.000
        TypVypisu=aktualni
        S_StavSubjektu=Aktivní
        S_Konkurz=0
        S_Vyrovnani=0
        S_Zamitnuti=0
        S_Likvidace=1
        ObchodniFirma=STOMAPLANT BR, s.r.o. v likvidaci
        Jmeno=NULL
        Prijmeni=NULL
        DatumNarozeni=NULL
        PF_Kody=112
        PF_Nazev=Společnost s ručením omezeným
        PF_Osoba=P
        PF_Text=Tuzemská
        A_IDAdresy=33996696
        A_KodStatu=203
        A_NazevStatu=Česká republika
        A_NazevOkresu=NULL
        A_NazevObce=Brno
        A_NazevCastiObce=Trnitá
        A_NazevUlice=Křenová
        A_CisloDomovni=479
        A_TypCisloDomovni=1
        A_CisloOrientacni=71
        A_PSC=60200
        DatumZapisu=2013-03-05 00:00:00.000
        MistoZapisu=Krajský soud v Brně
        ZnackaZapisu=C 78197
        */
        string sroXaver = "1453599";

        // string sroInsolvence = "25291441";
        // string euInsolvence = "24196444";

        AresEkonomickySubjekt? result = await aresClient.NactiEkonomickySubjektAsync(sroXaver);
        Assert.IsNotNull(result);

        if (result?.SeznamRegistraci?.InVr() == true)
        {
            AresVrEkonomickeSubjekty? test = await aresClient.NactiVrAsync(sroXaver);
            Assert.IsNotNull(test);
        }

        if (result?.SeznamRegistraci?.InRzp() == true)
        {
            AresRZP? rzp = await aresClient.NactiRZPAsync(result.Ico ?? throw new ArgumentNullException(nameof(result.Ico)));
        }

        /*
        if (result?.SeznamRegistraci?.InRes() == true)
        {
            Assert.IsTrue(true);
        }

        */
    }

}