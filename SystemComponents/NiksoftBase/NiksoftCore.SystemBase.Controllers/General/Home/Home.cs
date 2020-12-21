using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.SystemBase.Service;
using NiksoftCore.ViewModel;
using System.Diagnostics;

namespace NiksoftCore.SystemBase.Controllers.General.Home
{
    public class Home : NikController
    {
        private readonly ILogger<Home> _logger;

        public Home(ILogger<Home> logger, IConfiguration Configuration) : base(Configuration)
        {
            _logger = logger;
        }

        public IActionResult Index([FromQuery] string lang)
        {
            return View(GetViewName(lang, "Index"));
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CustomPage()
        {
            return View("Privacy");
        }

        private string GetViewName(string queryLang, string baseName)
        {
            var defaultLang = ISystemBaseServ.iPortalLanguageServ.Find(x => x.IsDefault);
            if (!string.IsNullOrEmpty(queryLang))
            {
                if (queryLang.ToLower() == "en")
                {
                    return baseName;
                }

                var defaultView = ISystemBaseServ.iPortalLanguageServ.Find(x => x.ShortName == queryLang);
                return defaultView.ShortName + baseName;
            }

            if (defaultLang.ShortName.ToLower() == "en")
            {
                return baseName;
            }

            return defaultLang.ShortName + baseName;
        }

    }
}
