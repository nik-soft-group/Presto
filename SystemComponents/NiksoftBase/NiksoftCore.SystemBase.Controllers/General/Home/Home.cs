using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NiksoftCore.SystemBase.Service;
using NiksoftCore.Utilities.Controllers;
using NiksoftCore.ViewModel;
using System.Diagnostics;

namespace NiksoftCore.SystemBase.Controllers.General.Home
{
    public class Home : NikController
    {
        private IConfiguration Config { get; }
        private readonly ILogger<Home> _logger;
        public ISystemBaseService iSystemBaseService { get; set; }


        public Home(ILogger<Home> logger, IConfiguration Configuration)
        {
            Config = Configuration;
            _logger = logger;
            iSystemBaseService = new SystemBaseService(Configuration);
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
            var defaultLang = iSystemBaseService.iPortalLanguageServ.Find(x => x.IsDefault);
            if (!string.IsNullOrEmpty(queryLang))
            {
                if (queryLang.ToLower() == "en")
                {
                    return baseName;
                }

                var defaultView = iSystemBaseService.iPortalLanguageServ.Find(x => x.ShortName == queryLang);
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
