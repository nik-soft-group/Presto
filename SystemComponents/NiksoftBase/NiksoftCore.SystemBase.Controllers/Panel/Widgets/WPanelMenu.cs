using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.SystemBase.Service;
using NiksoftCore.ViewModel.SystemBase;
using System.Collections.Generic;
using System.Linq;

namespace NiksoftCore.SystemBase.Controllers.Panel.Widgets
{
    public class WPanelMenu : ViewComponent
    {
        public IConfiguration Config { get; }
        public ISystemBaseService ISystemBaseServ { get; set; }
        private readonly UserManager<DataModel.User> userManager;

        public List<NikMessage> Messages;
        public PortalLanguage defaultLang;

        public WPanelMenu(IConfiguration Configuration, UserManager<DataModel.User> userManager)
        {
            Config = Configuration;
            Messages = new List<NikMessage>();
            ISystemBaseServ = new SystemBaseService(Configuration);
            defaultLang = ISystemBaseServ.iPortalLanguageServ.Find(x => x.IsDefault);
            this.userManager = userManager;
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
