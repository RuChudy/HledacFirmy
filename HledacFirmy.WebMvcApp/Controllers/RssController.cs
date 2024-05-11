using Hledac.Domain.Rss;
using Hledac.Domain.Rss.Services;
using HledacFirmy.WebMvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HledacFirmy.WebMvcApp.Controllers
{
    /// <summary>
    /// Řadič nad články.
    /// </summary>
    public class RssController : Controller
    {
        private readonly ILogger<RssController> _logger;
        private readonly IRssRepositoryService _rss;
        private readonly IRssReaderService _reader;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public RssController(ILogger<RssController> logger, IRssRepositoryService rss, IRssReaderService reader)
        {
            _logger = logger;
            _rss = rss;
            _reader = reader;
        }

        /// <summary>
        /// Nacte Rss feed z db a dle filtru upravi clanky.
        /// </summary>
        /// <param name="filter">filtr</param>
        /// <param name="cancel">zastaveni</param>
        /// <returns>RssFeed.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        [NonAction]
        private async Task<Feed?> GetFeedAsync(RssItemSearchModel filter, CancellationToken cancel)
        {
            try
            {
                int feedId = filter.FeedId ?? throw new ArgumentNullException(nameof(filter.FeedId));

                _logger.LogDebug($"Nahrávám rss feed={feedId}");
                Feed? rssFeed = await _rss.GetFeedByIdAsync(feedId);
                if (rssFeed is null)
                    return null;

                _logger.LogDebug($"Nalezen rss feed={feedId} ma clanku={rssFeed.Items}");
                if (rssFeed.Items?.Count is > 0)
                {
                    if (!string.IsNullOrWhiteSpace(filter?.SearchText))
                    {
                        rssFeed.Items = rssFeed.Items
                            .Where(i => i.Body?.Contains(filter.SearchText, StringComparison.CurrentCultureIgnoreCase) is true)
                            .ToList();
                    }

                    if (filter?.DateFrom is not null || filter?.DateTo is not null)
                    {
                        DateTime dateStart = (filter?.DateFrom ?? DateTime.MinValue).Date;
                        DateTime dateEnd = (filter?.DateTo ?? DateTime.Now).AddDays(1).Date;

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

                return rssFeed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get GetFeedByIdAsync fail.");
                throw new Exception($"Error reading feedId={filter?.FeedId}", ex);
            }
        }

        /// <summary>
        /// Zobrazí detail feedu s clanky.
        /// </summary>
        /// <param name="id">Id kanálu v databázi.</param>
        /// <returns>Detail kanálu s feedy.</returns>
        [HttpGet]
        public async Task<IActionResult> DetailAsync([FromRoute] int id, CancellationToken cancel)
        {
            var filter = new RssItemSearchModel { FeedId = id };
            ViewData["Filter"] = filter;

            Feed? feed = await GetFeedAsync(filter, cancel);
            return View(feed);
        }

        /// <summary>
        /// Zobrazí detail feedu s filtrem hledani v clancich.
        /// </summary>
        /// <param name="id">Id kanálu v databázi.</param>
        /// <returns>Detail kanálu s feedy.</returns>
        [HttpPost]
        public async Task<IActionResult> DetailAsync([FromRoute] int id, [FromForm] RssItemSearchModel filter, CancellationToken cancel)
        {
            if (!id.Equals(filter?.FeedId))
                throw new ArgumentOutOfRangeException("FeedId", filter?.FeedId, $"Hodnota nemá očekávanou hodnotu id={id}.");

            ViewData["Filter"] = filter;

            Feed? feed = await GetFeedAsync(filter, cancel);
            return View(feed);
        }

        /// <summary>
        /// Přidá další rss kanál.
        /// </summary>
        /// <param name="rssUri">Odkaz na zdroj rss kanálu.</param>
        /// <param name="cancellation">Zastavení.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAsync([FromForm] string rssUri, CancellationToken cancellation)
        {
            if (!Uri.TryCreate(rssUri, UriKind.Absolute, out Uri? validatedUri) || validatedUri is null)
            {
                _logger.LogWarning($"Chyba Url: '{rssUri}'.");
                throw new Exception("Neplatná url adresa.");
            }

            RssSiteUri rssSiteUri = new RssSiteUri { Uri = validatedUri.ToString() };
            RssCachedSite? site = await _rss.GetSiteAsync(rssSiteUri, cancellation);
            if (site == null)
            {
                Feed? newFeed = await _reader.GetFeedsAsync(rssSiteUri, cancellation);
                await _rss.AddOrUpdateAsync(rssSiteUri, newFeed, cancellation);
            }

            return RedirectToAction("Feeds", "Home");
        }

        /// <summary>
        /// Aktualizuje rss kanál.
        /// </summary>
        /// <param name="rssUri">Id rss kanálu k aktualizaci.</param>
        /// <param name="cancellation">Zastavení.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateAsync([FromForm] int updateRssId, CancellationToken cancellation)
        {
            RssCachedSite? site = await _rss.GetSiteByIdAsync(updateRssId, cancellation);
            if (site is not null)
            {
                Feed? actualFeed = await _reader.GetFeedsAsync(site.Site, cancellation);
                await _rss.AddOrUpdateAsync(site.Site, actualFeed, cancellation);
            }

            return RedirectToAction("Detail", new { id = updateRssId });
        }

        /// <summary>
        /// Smaže jeden nebo více rss kanálů.
        /// </summary>
        /// <param name="deleteRssId">Id jednoho kanálu nebo null.</param>
        /// <param name="deleteBulk">Seznam Id kanálů nebo null.</param>
        /// <param name="cancellation">Zastavení.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<IActionResult> DeleteAsync([FromForm] int? deleteRssId, [FromForm] string deleteBulk, IFormCollection formData, CancellationToken cancellation)
        {
            // pokud je hodnota deleteBulk true mazeme seznam zaskrtnutych rss kanalu.
            if ("true".Equals(deleteBulk, StringComparison.InvariantCulture))
            {
                // seznam vsech pozic kde je zaskrtnuty checkbox.
                List<int> posToDelete = FormDataKeysStartWith(formData, "bulk");
                if (posToDelete.Count > 0)
                {
                    // seznam id-cek ke smazani.
                    List<int> idRssFeeds = posToDelete.Select(i => FormDataReadIntValueFrom(formData, string.Concat("val", i))).ToList();

                    await _rss.BulkDeleteAsync(idRssFeeds, cancellation);
                }
            }
            else if (deleteRssId is not null)
            {
                // pokud je vyplnena hodnota deleteRssId mazeme jen jeden rss kanal
                await _rss.DeleteAsync(deleteRssId.Value, cancellation);
            }

            return RedirectToAction("Feeds", "Home");
        }

        /// <summary>Seznam pozic klíčů, které začínají na text.</summary>
        private static List<int> FormDataKeysStartWith(IFormCollection formData, string startWith)
        {
            return formData.Keys
                .Where(k => k.StartsWith(startWith, StringComparison.InvariantCulture))
                .Select(k => int.Parse(k.Substring(startWith.Length)))
                .ToList();
        }

        /// <summary>Hodnota typu int zaslaná ve formuláři.</summary>
        private static int FormDataReadIntValueFrom(IFormCollection formData, string key)
        {
            string? value = formData[key];
            if (value is null || !int.TryParse(value, out int id))
                throw new ArgumentOutOfRangeException(key, value, "Hodnota není typu int.");
            return id;
        }
    }
}
