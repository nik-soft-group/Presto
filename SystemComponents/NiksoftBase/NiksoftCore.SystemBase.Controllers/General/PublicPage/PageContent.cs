using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.SystemBase.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.SystemBase.Controllers.General.PublicPage
{
    public class PageContent : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        private readonly IWebHostEnvironment hosting;
        public PageContent(IConfiguration Configuration, IWebHostEnvironment hostingEnvironment,
            UserManager<DataModel.User> userManager) : base(Configuration)
        {
            this.userManager = userManager;
            hosting = hostingEnvironment;
        }

        public IActionResult Index(string Id, [FromQuery] string lang)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            int itemId = 0;
            GeneralContent pageItem;
            if (int.TryParse(Id, out itemId))
            {
                pageItem = ISystemBaseServ.iGeneralContentServ.Find(x => x.Id == itemId);
            }
            else
            {
                pageItem = ISystemBaseServ.iGeneralContentServ.Find(x => x.KeyValue == Id);
            }


            ViewBag.Content = pageItem;
            ViewData["Title"] = pageItem.Title;

            return View(GetViewName(lang, "Index"));
        }


    }
}
