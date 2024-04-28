namespace Hledac.Domain.Rss.Services;

public interface IRssReaderService
{
    Task<Feed> GetFeedsAsync(RssSite rssRequest);
}
