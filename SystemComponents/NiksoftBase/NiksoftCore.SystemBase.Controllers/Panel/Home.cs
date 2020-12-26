using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.MiddlController.Routing;
using NiksoftCore.SystemBase.Service;

namespace NiksoftCore.SystemBase.Controllers.Panel
{
    [Authorize]
    [Area("Panel")]
    public class Home : NikController
    {

        public Home(IConfiguration Configuration) : base(Configuration)
        {
        }

        public IActionResult Index([FromQuery] string lang)
        {
            if (defaultLang.ShortName.ToLower() == "fa")
                ViewBag.PageTitle = "داشبورد";
            else
                ViewBag.PageTitle = "Dashboard";
            return View(GetViewName(lang, "Index"));
        }
    }
}
