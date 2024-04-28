using System.Net;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;

namespace Hledac.Domain.Rss.Services;

/// <summary>
/// Klient na čtení RSS feedů z http zdroje.
/// </summary>
public class RssHttpClient
{
    private readonly ILogger<RssHttpClient> _logger;
    private readonly HttpClient _httpClient;

    public RssHttpClient(HttpClient httpClient, ILogger<RssHttpClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;

        _httpClient.DefaultRequestHeaders.Add("UserAgent", "RSS scraper");
        _httpClient.DefaultRequestHeaders.Add("Accept", "application /rss+xml");
        /* Accept: application/rss+xml, application/rdf+xml;q=0.8, application/atom+xml;q=0.6, application/xml;q=0.4, text/xml;q=0.4 */
    }

    /// <summary>
    /// Načte feed včetně článků ze zadané url adresy.
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public async Task<Feed> GetFeedsAsync(Uri address)
    {
        ArgumentNullException.ThrowIfNull(address);

        using var httpResponseMessage = await _httpClient.GetAsync(address);

        httpResponseMessage.EnsureSuccessStatusCode();

        using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

        return Feed.FromXDocument(XDocument.Load(stream: contentStream));
    }
}
