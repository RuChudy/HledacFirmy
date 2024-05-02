using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hledac.Domain.Rss.Services;

public class RssReaderService : IRssReaderService
{
    private readonly ILogger<RssReaderService> _logger;
    private readonly RssSettings _rssSettings;
    private readonly RssHttpClient _rssClient;

    public RssReaderService(IOptions<RssSettings> rssSettings, ILogger<RssReaderService> logger, RssHttpClient rssClient)
    {
        _rssSettings = rssSettings.Value;
        _logger = logger;
        _rssClient = rssClient;
    }

    public async Task<Feed> GetFeedsAsync(RssSiteUri rssRequest, CancellationToken cancellation)
    {
        ArgumentNullException.ThrowIfNull(rssRequest);

        Uri uri = new Uri(rssRequest.Uri, UriKind.Absolute);

        return await _rssClient.GetFeedsAsync(uri, cancellation);
    }
}
