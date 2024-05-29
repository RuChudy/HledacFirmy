using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hledac.Domain.Ares.Services;

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
    /// Najde v ARES firmu dle ičo.
    /// </summary>
    /// <param name="ico">Hledané ičo.</param>
    /// <returns>Podrobnosti firmy nebo null, pokud nenalezeno.</returns>
    public async Task<AresEkonomickySubjekt?> NactiEkonomickySubjektAsync(string ico)
    {
        Uri restUri = new Uri(string.Concat("ekonomicke-subjekty-v-be/rest/ekonomicke-subjekty/", ico), UriKind.Relative);

        AresEkonomickySubjekt? subject = await FromJson<AresEkonomickySubjekt>(restUri);
        if (subject is not null)
            _logger.LogDebug($"ARES RZP Nalezeno ico={subject?.IcoId}.");

        return subject;
    }

    /// <summary>
    /// Najde v RZP firmu dle ičo.
    /// </summary>
    /// <param name="ico">Hledané ičo.</param>
    /// <returns>Podrobnosti firmy nebo null, pokud nenalezeno.</returns>
    public async Task<AresRZP?> NactiRZPAsync(string ico)
    {
        Uri restUri = new Uri(string.Concat("ekonomicke-subjekty-v-be/rest/ekonomicke-subjekty-rzp/", ico), UriKind.Relative);

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
        Uri restUri = new Uri(string.Concat("ekonomicke-subjekty-v-be/rest/ekonomicke-subjekty-vr/", ico), UriKind.Relative);

        AresVrEkonomickeSubjekty? result = await FromJson<AresVrEkonomickeSubjekty>(restUri);
        if (result is not null)
            _logger.LogDebug($"ARES VR Nalezeno ico={result?.IcoId}.");

        return result;
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
