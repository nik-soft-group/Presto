using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NiksoftCore.MiddlController.Middles;

namespace NiksoftCore.ITCF.Conltroller.General.Business
{
    [Area("Business")]
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

    }
}
