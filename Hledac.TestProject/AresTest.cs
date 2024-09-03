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
using System.Text.Json;
using System.Text.Json.Serialization;

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

        var firmaSrvc = _host.Services.GetRequiredService<FirmaService>();
        Assert.IsInstanceOfType<FirmaService>(firmaSrvc);


        var filter = new AresFilterVr
        {
            Ico = new List<string>
            {
                /*
                "10234934",
                "10523278",
                "15503577",
                "16542142",
                "16662741",
                "24678988",
                "27312283", // posledni osoba

                "24313751",
                "02790599",
                "01453599",
                "25291441",
                "24196444",
                */
                "03347737",
                "01700189"
            }
        };

        AresVrRoot? resultVr = await aresClient.NactiVrDleFiltruAsync(filter);
        Assert.IsNotNull(resultVr);
        Assert.IsNotNull(resultVr.EkonomickeSubjekty);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        DirectoryInfo dir = new DirectoryInfo(@"C:\TempRuda\AresData");
        foreach (var subject in resultVr.EkonomickeSubjekty)
        {
            FileInfo aresFile = new FileInfo(Path.Combine(dir.FullName, String.Concat(subject.IcoId!, "-ares.json")));
            using (var writer = aresFile.CreateText())
                await writer.WriteAsync(JsonSerializer.Serialize(subject, jsonOptions));

            SubjectDbInfo dbInfo = Subject.Create(subject)!;
            SubjectVr? dbRow = firmaSrvc.UlozFirmaVrDoDatabaze(dbInfo);

            FileInfo dbFile = new FileInfo(Path.Combine(dir.FullName, String.Concat(subject.IcoId!, "-dbinf.json")));
            using (var writer = dbFile.CreateText())
                await writer.WriteAsync(JsonSerializer.Serialize(dbInfo, jsonOptions));
        }

        var subjekty = Subject.Create(resultVr.EkonomickeSubjekty)?.ToList();

        var found = subjekty?.Select(s => s?.ICO ?? string.Empty).ToList();
        var notFound = filter.Ico.Where(i => found?.Contains(i) is false).ToList();


        /*
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