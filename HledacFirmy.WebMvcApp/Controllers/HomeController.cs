using Hledac.Domain.Rss;
using Hledac.Domain.Rss.Services;
using HledacFirmy.WebMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HledacFirmy.WebMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRssRepositoryService _rss;
        private readonly IRssReaderService _reader;

        public HomeController(ILogger<HomeController> logger, IRssRepositoryService rss, IRssReaderService reader)
        {
            _logger = logger;
            _rss = rss;
            _reader = reader;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Zobrazí detail feedu se zprávami.
        /// </summary>
        /// <param name="id">Id kanálu v databázi.</param>
        /// <returns>Detail kanálu s feedy.</returns>
        [Route("feed/{id}")]
        public IActionResult Feed([FromRoute] int id)
        {
            return View("Feed", id);
        }

        /// <summary>
        /// Přidá další rss kanál.
        /// </summary>
        /// <param name="rssUri">Odkaz na zdroj rss kanálu.</param>
        /// <param name="cancellation">Zastavení.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RssCreate([FromForm] string rssUri, CancellationToken cancellation)
        {
            if(Uri.TryCreate(rssUri, UriKind.Absolute, out Uri? validatedUri) && validatedUri is not null)
            {
                RssSiteUri rssSiteUri = new RssSiteUri { Uri = validatedUri.ToString() };
                RssCachedSite? site = await _rss.GetSiteAsync(rssSiteUri, cancellation);
                if (site == null)
                {
                    Feed? newFeed = await _reader.GetFeedsAsync(rssSiteUri, cancellation);
                    await _rss.AddOrUpdateAsync(rssSiteUri, newFeed, cancellation);
                }
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Aktualizuje rss kanál.
        /// </summary>
        /// <param name="rssUri">Id rss kanálu k aktualizaci.</param>
        /// <param name="cancellation">Zastavení.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RssUpdate([FromForm] int updateRssId, CancellationToken cancellation)
        {
            RssCachedSite? site = await _rss.GetSiteByIdAsync(updateRssId, cancellation);
            if (site is not null)
            {
                Feed? actualFeed = await _reader.GetFeedsAsync(site.Site, cancellation);
                await _rss.AddOrUpdateAsync(site.Site, actualFeed, cancellation);
            }

            return RedirectToAction("feed", new { id = updateRssId });
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
        public async Task<IActionResult> RssDelete([FromForm] int? deleteRssId, [FromForm] string deleteBulk, IFormCollection formData, CancellationToken cancellation)
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

            return RedirectToAction("Index");
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
