using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Hledac.Database.Context;

namespace Hledac.Domain.Rss.Services;

/// <summary>
/// Služba nad ukládáním rss kanálů dodatabaze.
/// </summary>
public class RssRepositoryService : IRssRepositoryService
{
    private readonly ILogger<RssRepositoryService> _logger;
    private readonly SubjektDbContext _db;

    public RssRepositoryService(ILogger<RssRepositoryService> logger, SubjektDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    /// <summary>
    /// Seznam nesmazaných Rss webů v databázi.
    /// </summary>
    /// <param name="position">Od které pozice zero-based.</param>
    /// <param name="rowsCount">Kolik záznamů.</param>
    /// <returns>Seznam Rss webů v databázi.</returns>
    public async Task<ICollection<RssCachedSite>> GetAllSitesAsync(int position, int rows)
    {
        int calcPosition = int.Max(0, position);
        int calcRows = int.Max(0, int.Min(short.MaxValue, rows));

        return await _db.RssCacheFeeds
            .Where(x => x.Deleted == null)
            .OrderByDescending(x => x.Updated ?? x.Created)
            .Skip(position)
            .Take(rows)
            .Select(x => new RssCachedSite
            {
                Id = x.Id,
                Title = x.Title ?? x.SiteUri,
                Site = new RssSiteUri { Uri = x.SiteUri }
            })
            .ToListAsync();
    }

    /// <summary>
    /// Klíč nesmazaného Rss webu v datbázi.
    /// </summary>
    /// <param name="rssSite"></param>
    /// <returns>Klíč Rss webu v datbázi nebo null.</returns>
    public async Task<RssCachedSite?> GetSiteAsync(RssSiteUri rssSite)
    {
        ArgumentNullException.ThrowIfNull(rssSite);
        ArgumentNullException.ThrowIfNull(rssSite.Uri);

        string searchKey = rssSite.Uri;

        return await _db.RssCacheFeeds
            .Where(x => x.Deleted == null && searchKey.Equals(x.SiteUri))
            .Select(x => new RssCachedSite
            {
                Id = x.Id,
                Title = x.Title ?? x.SiteUri,
                Site = new RssSiteUri { Uri = x.SiteUri }
            })
            .SingleOrDefaultAsync();
    }

    /// <summary>
    /// Označí rss kanál jako smazaný.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <returns>Došlo ke smazaní záznamu?</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        RssCacheFeed? dataRow = await _db.RssCacheFeeds
            .Where(x => x.Id == id && x.Deleted == null)
            .SingleOrDefaultAsync();

        if (dataRow is null)
            return false;

        dataRow.Deleted = DateTime.UtcNow;

        return (await _db.SaveChangesAsync()) > 0;
    }

    /// <summary>
    /// Odstraní nenávratně záznam rss kanálu.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <returns>Počet smazaných záznamů.</returns>
    public async Task<int> RemoveAsync(int id)
    {
        RssCacheFeed? dataRow = await _db.RssCacheFeeds.Include(x => x.FeedItems)
            .Where(x => x.Id == id).SingleOrDefaultAsync();

        if (dataRow is null)
            return 0;

        _db.RssCacheFeedItems.RemoveRange(dataRow.FeedItems);
        _db.RssCacheFeeds.Remove(dataRow);

        return await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Načte Rss kanál uložený v db dle Id.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <returns>Rss kanál nebo null.</returns>
    public async Task<Feed?> GetFeedByIdAsync(int id)
    {
        RssCacheFeed? dataRow = await _db.RssCacheFeeds
            .Include(x => x.FeedItems)
            .Where(x => x.Id == id && x.Deleted == null)
            .SingleOrDefaultAsync();

        if (dataRow is null)
            return null;

        Uri? ToUri(string? url) => string.IsNullOrWhiteSpace(url) ? null : new Uri(url, UriKind.Absolute);

        return new Feed
        {
            Link = ToUri(dataRow.Link),
            Title = dataRow.Title,
            Language = dataRow.Language ?? "en",
            Copyright = dataRow.Copyright,
            Description = dataRow.Description,
            Items = dataRow.FeedItems.Select(i => new Item
                {
                    PublishDate = i.PublishDateUtc,
                    Guid = i.Guid,
                    Link = ToUri(i.Link),
                    Title = i.Title,
                    Body = i.Body,
                    Categories = i.OtherProperties.Categories
                })
                .ToList()
        };
    }

    /// <summary>
    /// Aktualizuje nebo vytvoří Rss Web kanálem.
    /// </summary>
    /// <param name="rssSite">Rss Web</param>
    /// <param name="feed">Rss kanál s článnky.</param>
    /// <returns>Počet aktualizovaných záznamů.</returns>
    public async Task<int> AddOrUpdateAsync(RssSiteUri rssSite, Feed feed)
    {
        ArgumentNullException.ThrowIfNull(rssSite);
        ArgumentNullException.ThrowIfNull(rssSite.Uri);
        ArgumentNullException.ThrowIfNull(feed);

        string searchKey = rssSite.Uri;

        RssCacheFeed? dataRow = await _db.RssCacheFeeds
            .Include(x => x.FeedItems)
            .Where(x => searchKey.Equals(x.SiteUri))
            .SingleOrDefaultAsync();

        if (dataRow is null)
        {
            // Zalozime novy feed
            dataRow = new RssCacheFeed
            {
                Created = DateTime.UtcNow,
                SiteUri = rssSite.Uri,
                Link = feed.Link?.ToString(),
                Title = feed.Title,
                Language = feed.Language,
                Copyright = feed.Copyright,
                Description = feed.Description
            };

            await _db.AddAsync(dataRow);
        }
        else
        {
            // Aktualizujeme existijici feed
            dataRow.Updated = DateTime.UtcNow;
            dataRow.Deleted = null;

            // Stare clanky mazeme.
            if (dataRow.FeedItems.Count > 0)
                _db.RssCacheFeedItems.RemoveRange(dataRow.FeedItems);
        }

        // Ulozime nove clanky
        dataRow.FeedItems.AddRange(
            feed.Items.Select(i => new RssCacheFeedItem
            {
                FeedId = 0,
                Feed = dataRow,

                Created = DateTime.UtcNow,
                PublishDateUtc = i.PublishDate,
                Guid = i.Guid,
                Link = i.Link?.ToString(),
                Title = i.Title?.Trim(),
                Body = i.Body,
                OtherProperties = new RssCacheFeedItemOther { Categories = i.Categories.ToList() }
            })
            .AsEnumerable()
        );

        return await _db.SaveChangesAsync();
    }
}
