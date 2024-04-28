using System.Xml.Linq;
using Microsoft.Extensions.Logging;

namespace Hledac.Domain.Rss.Services;

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

    public async Task<List<Item>> GetFeedsAsync(Uri address)
    {
        using var httpResponseMessage = await _httpClient.GetAsync(address);

        httpResponseMessage.EnsureSuccessStatusCode();

        using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

        var xdoc = XDocument.Load(stream: contentStream);

        var chanel = xdoc.Root?.Elements().Where(e => e.Name == "channel").SingleOrDefault() ?? throw new ArgumentNullException(nameof(xdoc.Root));

        List<Item> feeds = chanel.Elements().Where(e => e.Name == "item").Select(e => new Item
        {
            Title = (string?)e.Element("title"),
            Body = (string?)e.Element("description")
        })
        .ToList();

        return feeds;
    }
}
