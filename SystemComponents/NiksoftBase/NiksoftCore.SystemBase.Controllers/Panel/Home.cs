using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NiksoftCore.Utilities.Controllers;
using NiksoftCore.ViewModel;
using System.Diagnostics;

namespace NiksoftCore.SystemBase.Controllers.Panel
{
    [Authorize]
    [Area("Panel")]
    public class Home : NikController
    {
        private IConfiguration Config { get; }
        public Home(IConfiguration Configuration)
        {
            Config = Configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
