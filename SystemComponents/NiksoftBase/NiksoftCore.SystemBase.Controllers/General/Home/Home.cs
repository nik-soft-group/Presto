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
            iSystemBaseService = new SystemBaseService();
        }

        public IActionResult Index()
        {
            //ISystemUnitOfWork uow = new SystemBaseDbContext();
            //ISystemSettingService iSystemSettingServ = new SystemSettingService(uow);
            //var x = iSystemSettingServ.GetPart(x => true, 0, 20);
            var data = iSystemBaseService.iSystemSettingServ.GetPart(x => true, 0, 20);
            return View();
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
    }
}
