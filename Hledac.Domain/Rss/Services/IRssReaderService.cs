namespace Hledac.Domain.Rss.Services;

public interface IRssReaderService
{
    /// <summary>
    /// Přečte rss kanál z webu.
    /// </summary>
    /// <param name="rssRequest">Rss web.</param>
    /// <returns>Načtený feed z webu.</returns>
    Task<Feed> GetFeedsAsync(RssSite rssRequest);
}
