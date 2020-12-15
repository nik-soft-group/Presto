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

        public IActionResult Index([FromQuery] string lang)
        {
            return View(GetViewName(new string[] { lang, "Index", "FaIndex" }));
        }

        private string GetViewName(string[] names)
        {
            string lang = names[0];
            if (string.IsNullOrEmpty(lang))
            {
                return names[1];
            }
            else if (lang == "fa")
            {
                return names[2];
            }
            else
            {
                return names[1];
            }
        }
    }
}
