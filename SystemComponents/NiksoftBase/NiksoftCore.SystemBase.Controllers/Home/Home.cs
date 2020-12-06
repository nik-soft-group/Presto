using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NiksoftCore.Utilities.Controllers;
using NiksoftCore.ViewModel;
using System.Diagnostics;

namespace NiksoftCore.SystemBase.Controllers.Home
{
    public class Home : NikController
    {
        private IConfiguration Config { get; }
        private readonly ILogger<Home> _logger;
        public Home(ILogger<Home> logger, IConfiguration Configuration)
        {
            Config = Configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

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
    }
}
