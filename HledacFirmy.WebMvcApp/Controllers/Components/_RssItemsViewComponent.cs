using Microsoft.AspNetCore.Mvc;
using Hledac.Domain.Rss;
using Hledac.Domain.Rss.Services;
using HledacFirmy.WebMvcApp.Models;

namespace HledacFirmy.WebMvcApp.Controllers.Components;

/// <summary>
/// Komponenta pro zobrazení článků RSS.
/// </summary>
[ViewComponent(Name = "_RssItems")]
public class _RssItemsViewComponent : ViewComponent
{
    private readonly ILogger<_RssItemsViewComponent> _logger;
    private readonly IRssRepositoryService _rss;

    /// <summary>
    /// Konstruktor.
    /// </summary>
    /// <param name="logger">Loger.</param>
    /// <param name="rss">Repozitář.</param>
    public _RssItemsViewComponent(ILogger<_RssItemsViewComponent> logger, IRssRepositoryService rss)
    {
        _logger = logger;
        _rss = rss;
    }

    /// <summary>
    /// Vykresli kontrol.
    /// </summary>
    /// <param name="rssModel">Výběr článků.</param>
    /// <returns>Výsledek.</returns>
    public async Task<IViewComponentResult> InvokeAsync(RssItemSearchModel? rssModel)
    {
        try
        {
            Feed? rssFeed = (rssModel?.FeedId is null) ? null : await _rss.GetFeedByIdAsync(rssModel.FeedId.Value);
            if (rssFeed is null)
                return View("_RssItems");

            if (rssFeed.Items?.Count is > 0)
            {
                if (!string.IsNullOrWhiteSpace(rssModel?.SearchText))
                {
                    rssFeed.Items = rssFeed.Items
                        .Where(i => i.Body?.Contains(rssModel.SearchText, StringComparison.CurrentCultureIgnoreCase) is true)
                        .ToList();
                }

                if (rssModel?.DateFrom is not null || rssModel?.DateTo is not null)
                {
                    DateTime dateStart = (rssModel?.DateFrom ?? DateTime.MinValue).Date;
                    DateTime dateEnd = (rssModel?.DateTo ?? DateTime.Now).AddDays(1).Date;

                    if (dateEnd < dateStart)
                    {
                        DateTime tmp = dateEnd;
                        dateEnd = dateStart;
                        dateStart = tmp;
                    }

                    rssFeed.Items = rssFeed.Items
                        .Where(i => i.PublishDate is not null && i.PublishDate >= dateStart && i.PublishDate < dateEnd)
                        .ToList();
                }
            }

            return View("_RssItems", rssFeed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get GetFeedByIdAsync fail.");
            return View("_ItemsError", ex.Message);
        }
    }
}
