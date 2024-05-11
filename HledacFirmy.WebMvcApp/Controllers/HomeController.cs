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
        /// Zobrazi seznam rss feedu.
        /// </summary>
        /// <returns>Seznam kanálů s feedy.</returns>
        public async Task<IActionResult> FeedsAsync(CancellationToken cancel)
        {
            try
            {
                ICollection<RssCachedSite> rssFeeds = await _rss.GetAllSitesAsync(0, 100, cancel);
                return View("../Rss/Feeds", rssFeeds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get GetAllSitesAsync fail.");
                throw new Exception("Read all feeds fail!", ex);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
