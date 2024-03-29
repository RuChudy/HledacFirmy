﻿using System.Net;
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
    public async Task<AresEkonomickySubjekt?> NajdiEkonomickySubjektAsync(string ico)
    {
        string restUri = string.Concat("ekonomicke-subjekty-v-be/rest/ekonomicke-subjekty/", ico);

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

            AresEkonomickySubjekt? result = await JsonSerializer.DeserializeAsync<AresEkonomickySubjekt>(utf8Json: contentStream,
                options: new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNameCaseInsensitive = true
                });

            if (result == null)
                throw new HttpRequestException("HTTP Odpověď formát JSON nelze rekonstruovat.", null, HttpStatusCode.UnsupportedMediaType);

            _logger.LogDebug($"ARES Nalezeno ico={result.ico} '{result.obchodniJmeno}'.");
            return result;
        }
        else
        {
            throw new HttpRequestException("HTTP Odpověď byla neplatná a nelze ji rekonstruovat.", null, HttpStatusCode.UnsupportedMediaType);
        }
    }
}
