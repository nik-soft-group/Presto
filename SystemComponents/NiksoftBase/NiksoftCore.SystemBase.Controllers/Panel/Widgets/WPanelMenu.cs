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
        public ISystemBaseService ISystemBaseServ { get; set; }
        private readonly PortalLanguage defaultLang;

        public WPanelMenu(IConfiguration Configuration, UserManager<DataModel.User> userManager)
        {
            this.userManager = userManager;
            ISystemBaseServ = new SystemBaseService(Configuration);
            defaultLang = ISystemBaseServ.iPortalLanguageServ.Find(x => x.IsDefault);
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                //var thisUser = userManager.GetUserAsync(HttpContext.User).Result;

                ViewBag.Menus = ISystemBaseServ.iPanelMenuService.GetPart(x => x.Enabled && x.ParentId == null, 0, 50).ToList();
            }

            
            return View();
        }

    }
}
