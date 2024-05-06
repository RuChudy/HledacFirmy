using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Hledac.Database.Context;
using System.Linq;
using System.Net;
using System.Data;

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
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Seznam Rss webů v databázi.</returns>
    public async Task<ICollection<RssCachedSite>> GetAllSitesAsync(int position, int rows, CancellationToken cancellation)
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
            .ToListAsync(cancellation);
    }

    /// <summary>
    /// Klíč nesmazaného Rss webu v datbázi.
    /// </summary>
    /// <param name="rssSite"></param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Klíč Rss webu v datbázi nebo null.</returns>
    public async Task<RssCachedSite?> GetSiteAsync(RssSiteUri rssSite, CancellationToken cancellation)
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
            .SingleOrDefaultAsync(cancellation);
    }

    /// <summary>
    /// Označí rss kanál jako smazaný.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Došlo ke smazaní záznamu?</returns>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellation)
    {
        RssCacheFeed? dataRow = await _db.RssCacheFeeds
            .Where(x => x.Id == id && x.Deleted == null)
            .SingleOrDefaultAsync(cancellation);

        if (dataRow is null)
            return false;

        dataRow.Deleted = DateTime.UtcNow;

        _logger.LogDebug($"Značím rss kanál Id={dataRow.Id} jako smazaný..");
        return (await _db.SaveChangesAsync(cancellation)) > 0;
    }

    /// <summary>
    /// Označí několik rss kanálů jako smazaný.
    /// </summary>
    /// <param name="keys">Seznam Id rss kanálů.</param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Počet smazaných záznamů.</returns>
    public async Task<int> BulkDeleteAsync(IEnumerable<int> keys, CancellationToken cancellation)
    {
        ArgumentNullException.ThrowIfNull(keys);

        if (!keys.Any())
            return 0;

        DateTime timeToDelete = DateTime.UtcNow;

        var info = String.Join(",", keys.Take(10).Select(k => k.ToString()).AsEnumerable());
        _logger.LogDebug($"Značím rss kanály Ids={info} jako smazané..");

        return await _db.RssCacheFeeds.Where(r => keys.Contains(r.Id))
            .ExecuteUpdateAsync(r => r.SetProperty(e => e.Deleted, timeToDelete), cancellation);
    }

    /// <summary>
    /// Odstraní nenávratně záznam rss kanálu.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Počet smazaných záznamů.</returns>
    public async Task<int> RemoveAsync(int id, CancellationToken cancellation)
    {
        RssCacheFeed? dataRow = await _db.RssCacheFeeds.Include(x => x.FeedItems)
            .Where(x => x.Id == id).SingleOrDefaultAsync();

        if (dataRow is null)
            return 0;

        _logger.LogDebug($"Odstraňuji rss kanál Id={dataRow.Id}..");

        using (var transaction = _db.Database.BeginTransaction())
        {
            _db.RssCacheFeedItems.RemoveRange(dataRow.FeedItems);
            _db.RssCacheFeeds.Remove(dataRow);

            var removed = await _db.SaveChangesAsync(cancellation);

            transaction.Commit();
            return removed;
        }
    }

    /// <summary>
    /// Odstraní nenávratně rss kanály, které jsou označené jako smazané.
    /// </summary>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Počet smazaných záznamů.</returns>
    public async Task<int> BatchRemoveDeletedAsync(CancellationToken cancellation)
    {
        var deletedCount = await _db.RssCacheFeeds.CountAsync(r => r.Deleted != null, cancellation);
        if (deletedCount <= 0)
            return 0;

        cancellation.ThrowIfCancellationRequested();

        _logger.LogDebug("Odstraňuji smazané rss kanály..");

        using (var transaction = _db.Database.BeginTransaction())
        {
            int deletedRows = await _db.RssCacheFeedItems.Where(r => r.Feed.Deleted == null).ExecuteDeleteAsync(cancellation);
            deletedRows += await _db.RssCacheFeeds.Where(r => r.Deleted != null).ExecuteDeleteAsync(cancellation);

            cancellation.ThrowIfCancellationRequested();
            transaction.Commit();

            return deletedRows;
        }
    }

    /// <summary>
    /// Nesmazaný klíč Rss webu v databázi podle Id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Klíč Rss webu v datbázi nebo null.</returns>
    public async Task<RssCachedSite?> GetSiteByIdAsync(int id, CancellationToken cancellation)
    {
        return await _db.RssCacheFeeds
            .Where(x => x.Id == id && x.Deleted == null)
            .Select(x => new RssCachedSite
            {
                Id = x.Id,
                Title = x.Title ?? x.SiteUri,
                Site = new RssSiteUri { Uri = x.SiteUri }
            })
            .SingleOrDefaultAsync(cancellation);
    }

    /// <summary>
    /// Načte Rss kanál uložený v db dle Id.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Rss kanál nebo null.</returns>
    public async Task<Feed?> GetFeedByIdAsync(int id, CancellationToken cancellation)
    {
        RssCacheFeed? dataRow = await _db.RssCacheFeeds
            .Include(x => x.FeedItems)
            .Where(x => x.Id == id && x.Deleted == null)
            .SingleOrDefaultAsync(cancellation);

        if (dataRow is null)
            return null;

        _logger.LogDebug($"Čtu rss kanál Id={dataRow.Id} '{dataRow.Title}'[{dataRow.FeedItems.Count}] z databáze..");

        Uri? ToUri(string? url) => string.IsNullOrWhiteSpace(url) ? null : new Uri(url, UriKind.Absolute);

        return new Feed
        {
            Id = dataRow.Id,
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
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Počet aktualizovaných záznamů.</returns>
    public async Task<int> AddOrUpdateAsync(RssSiteUri rssSite, Feed feed, CancellationToken cancellation)
    {
        ArgumentNullException.ThrowIfNull(rssSite);
        ArgumentNullException.ThrowIfNull(rssSite.Uri);
        ArgumentNullException.ThrowIfNull(feed);

        string searchKey = rssSite.Uri;

        RssCacheFeed? dataRow = await _db.RssCacheFeeds
            .Include(x => x.FeedItems)
            .Where(x => searchKey.Equals(x.SiteUri))
            .SingleOrDefaultAsync(cancellation);

        using (var transaction = _db.Database.BeginTransaction())
        {
            if (dataRow is null)
            {
                // Zalozime novy feed
                _logger.LogDebug("Vytvářím nový rss kanál..");

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
                _logger.LogDebug($"Aktualizuji rss kanál Id={dataRow.Id}..");

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

            int changedRows = await _db.SaveChangesAsync(cancellation);
            transaction.Commit();

            _logger.LogDebug($"Rss kanál uložen Id={dataRow.Id}.");
            return changedRows;
        }
    }
}
