using Microsoft.AspNetCore.Identity;
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
    public class WSlider : ViewComponent
    {
        public IConfiguration Config { get; }
        public ISystemBaseService ISystemBaseServ { get; set; }
        private readonly UserManager<DataModel.User> userManager;

        public List<NikMessage> Messages;
        public PortalLanguage defaultLang;

        public WSlider(IConfiguration Configuration, UserManager<DataModel.User> userManager)
        {
            Config = Configuration;
            Messages = new List<NikMessage>();
            ISystemBaseServ = new SystemBaseService(Config.GetConnectionString("SystemBase"));
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var slidrData = ISystemBaseServ.iGeneralContentServ.GetAll(x => x.ContentCategory.KeyValue.Contains("mainslider"));
            string frame = "<span class='element' ";
            int cc1 = 0;
            foreach (var item in slidrData)
            {
                cc1++;
                frame += "data-text"+ cc1 + "='"+ item.Header +"'";
            }

            frame += "data-loop='true' data-backdelay='3000'></span>";
            ViewBag.SliderText = frame;
            ViewBag.Contents = slidrData.ToList();
            return View();
        }

    }
}
