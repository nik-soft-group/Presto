using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NiksoftCore.SystemBase.Service;
using NiksoftCore.Utilities.Controllers;
using NiksoftCore.ViewModel;
using System.Collections.Generic;
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
            List<LanguageViewModel> views = new List<LanguageViewModel>();
            views.Add(new LanguageViewModel { 
                ShortName = "en",
                ViewName = "Index",
                IsDefault = true
            });

            views.Add(new LanguageViewModel
            {
                ShortName = "fa",
                ViewName = "FaIndex",
                IsDefault = true
            });

            return View(GetViewName(lang, views));
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

        private string GetViewName(string queryLang, List<LanguageViewModel> views)
        {
            if (!string.IsNullOrEmpty(queryLang))
            {
                var defaultView = views.Find(x => x.ShortName == queryLang);
                return defaultView.ViewName;
            }

            var defaultLang = iSystemBaseService.iPortalLanguageServ.Find(x => x.IsDefault);
            var viewItem = views.Find(x => x.ShortName == defaultLang.ShortName);
            return viewItem.ViewName;
        }

    }
}
