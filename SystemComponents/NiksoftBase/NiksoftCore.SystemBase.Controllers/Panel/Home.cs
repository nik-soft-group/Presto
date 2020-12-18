using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.SystemBase.Service;

namespace NiksoftCore.SystemBase.Controllers.Panel
{
    [Authorize]
    [Area("Panel")]
    public class Home : NikController
    {

        public Home(IConfiguration Configuration): base(Configuration)
        {
            iSystemBaseService = new SystemBaseService(Configuration);
        }

        public IActionResult Index([FromQuery] string lang)
        {
            return View(GetViewName(lang, "Index"));
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
