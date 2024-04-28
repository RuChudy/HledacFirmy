namespace Hledac.Domain.Rss.Services;

public interface IRssService
{
    Task<List<Item>> GetFeedsAsync(RssRequest rssRequest);
}
