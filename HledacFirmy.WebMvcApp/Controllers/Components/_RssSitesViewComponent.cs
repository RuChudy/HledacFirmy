using Microsoft.AspNetCore.Mvc;
using Hledac.Domain.Rss;
using Hledac.Domain.Rss.Services;

namespace HledacFirmy.WebMvcApp.Controllers.Components;

/// <summary>
/// Komponenta pro zobrazení všech Rss kanálů.
/// </summary>
[ViewComponent(Name = "_RssSites")]
public class _RssSitesViewComponent : ViewComponent
{
    private readonly ILogger<_RssSitesViewComponent> _logger;
    private readonly IRssRepositoryService _rss;

    /// <summary>
    /// Konstruktor.
    /// </summary>
    /// <param name="logger">Loger.</param>
    /// <param name="rss">Repozitář.</param>
    public _RssSitesViewComponent(ILogger<_RssSitesViewComponent> logger, IRssRepositoryService rss)
    {
        _logger = logger;
        _rss = rss;
    }

    /// <summary>
    /// Vykresli kontrol - Seznam Rss kanálů.
    /// </summary>
    /// <returns>Výsledek.</returns>
    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancel)
    {
        try
        {
            ICollection<RssCachedSite> rssFeeds = await _rss.GetAllSitesAsync(0, 100, cancel);
            return View("_RssSites", rssFeeds);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get GetAllSitesAsync fail.");
            return View("_RssError", ex.Message);
        }
    }
}
