using Hledac.Database.Context;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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

    public async Task<IEnumerable<Feed>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Feed> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(RssSite rssSite, Feed feed)
    {
        ArgumentNullException.ThrowIfNull(rssSite);
        ArgumentNullException.ThrowIfNull(rssSite.Uri);
        ArgumentNullException.ThrowIfNull(feed);

        var newFeed = new RssCacheFeed
        {
            Created = DateTime.UtcNow,
            SiteUri = rssSite.Uri,
            Link = feed.Link?.ToString(),
            Title = feed.Title,
            Language = feed.Language,
            Copyright = feed.Copyright,
            Description = feed.Description
        };

        newFeed.FeedItems.AddRange(
            feed.Items.Select(i => new RssCacheFeedItem
            {
                FeedId = 0,
                Feed = newFeed,

                Created = DateTime.UtcNow,
                PublishDate = i.PublishDate,
                Guid = i.Guid,
                Link = i.Link?.ToString(),
                Title = i.Title?.Trim(),
                Body = i.Body,
                Categories = new RssCacheCategories { Categories = i.Categories.ToList() }
            })
            .AsEnumerable()
        );

        await _db.AddAsync( newFeed );
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Feed feed)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
