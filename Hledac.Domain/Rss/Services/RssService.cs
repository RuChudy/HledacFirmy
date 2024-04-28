using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hledac.Domain.Rss.Services;

public class RssService : IRssService
{
    private readonly ILogger<RssService> _logger;
    private readonly RssSettings _rssSettings;
    private readonly RssHttpClient _rssClient;

    public RssService(IOptions<RssSettings> rssSettings, ILogger<RssService> logger, RssHttpClient rssClient)
    {
        _rssSettings = rssSettings.Value;
        _logger = logger;
        _rssClient = rssClient;
    }

    public async Task<List<Item>> GetFeedsAsync(RssRequest rssRequest)
    {
        Uri uri = new Uri("https://www.ceskenoviny.cz/sluzby/rss/cr.php", UriKind.Absolute);

        return await _rssClient.GetFeedsAsync(uri);
    }
}
