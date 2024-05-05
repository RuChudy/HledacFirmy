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

        [Route("feed/{id}")]
        public IActionResult Feed([FromRoute] int id)
        {
            return View("Feed", id);
        }

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

        [HttpPost]
        public async Task<IActionResult> RssDelete([FromForm] int? deleteRssId, [FromForm] string deleteBulk, CancellationToken cancellation)
        {
            int RequestIntValue(int i)
            {
                string key = string.Concat("val", i);
                string? value = Request?.Form?[key];
                if (value is null || !int.TryParse(value, out int id))
                    throw new ArgumentNullException();
                return id;
            }

            if ("true".Equals(deleteBulk, StringComparison.InvariantCulture))
            {
                List<string> keysToDelete = Request.Form.Keys.Where(k => k.StartsWith("bulk", StringComparison.InvariantCulture)).ToList();
                if (keysToDelete.Count > 0)
                {
                    List<int> positionToDelete = keysToDelete.Select(s => int.Parse(s.Substring(4))).ToList();
                    List<int> idvalues = positionToDelete.Select(i => RequestIntValue(i)).ToList();

                    await _rss.BulkDeleteAsync(idvalues, cancellation);
                }
            }
            else if (deleteRssId is not null)
            {
                await _rss.DeleteAsync(deleteRssId.Value, cancellation);
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
