using Microsoft.AspNetCore.Mvc;
using Hledac.Domain.Rss;
using Hledac.Domain.Rss.Services;

namespace HledacFirmy.WebMvcApp.Controllers.Components;

/// <summary>
/// Komponenta pro zobrazení článků RSS.
/// </summary>
[ViewComponent(Name = "_RssItems")]
public class _RssItemsViewComponent : ViewComponent
{
    private readonly ILogger<_RssSitesViewComponent> _logger;
    private readonly IRssRepositoryService _rss;

    /// <summary>
    /// Konstruktor.
    /// </summary>
    /// <param name="logger">Loger.</param>
    /// <param name="rss">Repozitář.</param>
    public _RssItemsViewComponent(ILogger<_RssSitesViewComponent> logger, IRssRepositoryService rss)
    {
        _logger = logger;
        _rss = rss;
    }

    /// <summary>
    /// Vykresli kontrol.
    /// </summary>
    /// <param name="pager">Výběr stránky.</param>
    /// <returns>Výsledek.</returns>
    public async Task<IViewComponentResult> InvokeAsync(int? rssId)
    {
        try
        {
            Feed? rssFeed = (rssId is null) ? null : (await _rss.GetFeedByIdAsync(rssId.Value));
            return View("_RssItems", rssFeed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get GetFeedByIdAsync fail.");
            return View("_RssError", ex.Message);
        }
    }
}
