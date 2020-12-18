using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.SystemBase.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.SystemBase.Controllers.Panel.Widgets
{
    public class WPanelMenu : ViewComponent
    {
        private readonly UserManager<DataModel.User> userManager;
        public ISystemBaseService iSystemBaseService { get; set; }
        private PortalLanguage defaultLang;

        public WPanelMenu(IConfiguration Configuration, UserManager<DataModel.User> userManager)
        {
            this.userManager = userManager;
            iSystemBaseService = new SystemBaseService(Configuration);
            defaultLang = iSystemBaseService.iPortalLanguageServ.Find(x => x.IsDefault);
        }

        public async Task<IViewComponentResult> LoadWidget()
        {
            if (User.Identity.IsAuthenticated)
            {
                var thisUser = await userManager.GetUserAsync(HttpContext.User);

                ViewBag.Menus = iSystemBaseService.iPanelMenuService.GetPart(x => x.Enabled, 0, 50);
            }

            
            return View();
        }

    }
}
