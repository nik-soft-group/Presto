using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NiksoftCore.MiddlController.Middles;
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
            if (string.IsNullOrEmpty(lang))
                lang = defaultLang.ShortName;

            return View(GetViewName(lang, "Index"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
