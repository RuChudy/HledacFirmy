using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HledacFirmy.Ares.Services;

/// <summary>
/// Http Client pro vyhledání subjektu v sytému ARES.
/// </summary>
public class AresHttpClient
{
    private readonly ILogger<AresHttpClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly AresSettings _aresSettings;

    /// <summary>
    /// Konstruktor.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    /// <param name="logger">Logger.</param>
    /// <param name="aresSettings">Nastavení ARES.</param>
    public AresHttpClient(HttpClient httpClient, ILogger<AresHttpClient> logger, IOptions<AresSettings> aresSettings)
    {
        _httpClient = httpClient;
        _logger = logger;
        _aresSettings = aresSettings.Value;

        if (string.IsNullOrEmpty(_aresSettings?.Uri))
            throw new ArgumentNullException(nameof(aresSettings));

        _httpClient.BaseAddress = new Uri(_aresSettings.Uri);

        _httpClient.DefaultRequestHeaders.Add("UserAgent", "Ares scraper");
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    /// <summary>
    /// Kontroluje format ic a pripadne doplni o nuly.
    /// </summary>
    /// <param name="ico">ico.</param>
    /// <returns>platny format ic.</returns>
    private static string ToUriIco(string ico)
    {
        if (string.IsNullOrWhiteSpace(ico) || ico.Length > 8 || !ico.All(char.IsDigit))
            throw new ArgumentNullException(nameof(ico));

        return ico.PadLeft(8, '0');
    }

    /// <summary>
    /// Najde v ARES firmu dle ičo.
    /// </summary>
    /// <param name="ico">Hledané ičo.</param>
    /// <returns>Podrobnosti firmy nebo null, pokud nenalezeno.</returns>
    public async Task<AresEkonomickySubjekt?> NactiEkonomickySubjektAsync(string ico)
    {
        Uri restUri = new Uri(string.Concat("ekonomicke-subjekty-v-be/rest/ekonomicke-subjekty/", ToUriIco(ico)), UriKind.Relative);

        AresEkonomickySubjekt? subject = await FromJson<AresEkonomickySubjekt>(restUri);
        if (subject is not null)
            _logger.LogDebug($"ARES SUB Nalezeno ico={subject?.IcoId}.");

        return subject;
    }

    /// <summary>
    /// Najde v RZP firmu dle ičo.
    /// </summary>
    /// <param name="ico">Hledané ičo.</param>
    /// <returns>Podrobnosti firmy nebo null, pokud nenalezeno.</returns>
    public async Task<AresRZP?> NactiRZPAsync(string ico)
    {
        Uri restUri = new Uri(string.Concat("ekonomicke-subjekty-v-be/rest/ekonomicke-subjekty-rzp/", ToUriIco(ico)), UriKind.Relative);

        AresRZP? result = await FromJson<AresRZP>(restUri);
        if (result is not null)
            _logger.LogDebug($"ARES RZP Nalezeno ico={result?.IcoId}.");

        return result;
    }

    /// <summary>
    /// Najde v VR firmu dle ičo.
    /// </summary>
    /// <param name="ico">Hledané ičo.</param>
    /// <returns>Podrobnosti firmy nebo null, pokud nenalezeno.</returns>
    public async Task<AresVrEkonomickeSubjekty?> NactiVrAsync(string ico)
    {
        Uri restUri = new Uri(string.Concat("ekonomicke-subjekty-v-be/rest/ekonomicke-subjekty-vr/", ToUriIco(ico)), UriKind.Relative);

        AresVrEkonomickeSubjekty? result = await FromJson<AresVrEkonomickeSubjekty>(restUri);
        if (result is not null)
            _logger.LogDebug($"ARES VR Nalezeno ico={result?.IcoId}.");

        return result;
    }

    /// <summary>
    /// Najde v VR firmy dle filtru ičo.
    /// </summary>
    /// <param name="filter">Filtr hledání.</param>
    /// <returns>Podrobnosti firem.</returns>
    public async Task<AresVrRoot?> NactiVrDleFiltruAsync(AresFilterVr filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        Uri restUri = new Uri("ekonomicke-subjekty-v-be/rest/ekonomicke-subjekty-vr/vyhledat", UriKind.Relative);

        string jsonText = JsonSerializer.Serialize(filter);

        using var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
        using var request = new HttpRequestMessage(HttpMethod.Post, restUri) { Content = content };

        _logger.LogDebug($"ARES kontaktuji '{_httpClient.BaseAddress}{restUri}'..");
        using var httpResponseMessage = await _httpClient.SendAsync(request);

        httpResponseMessage.EnsureSuccessStatusCode(); // throws if not 200-299

        if (!(httpResponseMessage.Content is object) || !(httpResponseMessage.Content?.Headers?.ContentType?.MediaType?.Equals("application/json", StringComparison.InvariantCultureIgnoreCase) is true))
            throw new HttpRequestException("HTTP Odpověď formátu JSON nelze rekonstruovat.", null, HttpStatusCode.UnsupportedMediaType);

        using Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<AresVrRoot>(
            utf8Json: contentStream,
            options: new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true
            });
    }

    private async Task<T?> FromJson<T>(Uri restUri) where T : class, new()
    {
        _logger.LogDebug($"ARES kontaktuji '{_httpClient.BaseAddress}{restUri}'..");
        using var httpResponseMessage = await _httpClient.GetAsync(restUri);

        if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogInformation($"ARES StatusCode=404 ičo nenalezeno.");
            return null;
        }
        else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
        {
            var errMessage = $"ARES StatusCode=400 chybný vstupní formát ič.";
            _logger.LogError(errMessage);
            throw new Exception(errMessage);
        }

        httpResponseMessage.EnsureSuccessStatusCode(); // throws if not 200-299

        if (httpResponseMessage.Content is object && httpResponseMessage.Content.Headers?.ContentType?.MediaType == "application/json")
        {
            using Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            T? result = await JsonSerializer.DeserializeAsync<T>(
                utf8Json: contentStream,
                options: new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNameCaseInsensitive = true
                });

            if (result == null)
                throw new HttpRequestException("HTTP Odpověď formát JSON nelze rekonstruovat.", null, HttpStatusCode.UnsupportedMediaType);

            return result;
        }
        else
        {
            throw new HttpRequestException("HTTP Odpověď byla neplatná a nelze ji rekonstruovat.", null, HttpStatusCode.UnsupportedMediaType);
        }
    }

}
