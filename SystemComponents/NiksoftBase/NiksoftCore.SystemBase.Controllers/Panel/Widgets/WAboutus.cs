﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.SystemBase.Service;
using NiksoftCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.SystemBase.Controllers.Panel.Widgets
{
    public class WAboutus : ViewComponent
    {
        public IConfiguration Config { get; }
        public ISystemBaseService ISystemBaseServ { get; set; }
        private readonly UserManager<DataModel.User> userManager;

        public List<NikMessage> Messages;
        public PortalLanguage defaultLang;

        public WAboutus(IConfiguration Configuration, UserManager<DataModel.User> userManager)
        {
            Config = Configuration;
            Messages = new List<NikMessage>();
            ISystemBaseServ = new SystemBaseService(Config.GetConnectionString("SystemBase"));
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var aboutItem = ISystemBaseServ.iGeneralContentServ.GetAll(x => x.ContentCategory.KeyValue.Contains("aboutus")).FirstOrDefault();
            var aboutIcons = ISystemBaseServ.iGeneralContentServ.GetAll(x => x.ContentCategory.KeyValue.Contains("abouticons"));
            ViewBag.About = aboutItem;
            ViewBag.Icons = aboutIcons;
            return View();
        }

    }
}
