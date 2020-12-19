using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.SystemBase.Service;
using NiksoftCore.ViewModel.SystemBase;
using System.Linq;
using System.Threading.Tasks;

namespace NiksoftCore.SystemBase.Controllers.Panel.Modules
{
    [Area("Panel")]
    [Authorize]
    public class PanelMenuManage : NikController
    {

        public PanelMenuManage(IConfiguration Configuration) : base(Configuration)
        {

        }

        public IActionResult Index([FromQuery] string lang)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (lang == "fa")
                ViewBag.PageTitle = "مدیریت منوها";
            else
                ViewBag.PageTitle = "Menu Management";

            ViewBag.Contents = iSystemBaseService.iPanelMenuService.GetPart(x => x.ParentId == null, 0, 20).ToList();
            return View(GetViewName(lang, "Index"));
        }

        [HttpGet]
        public IActionResult Create([FromQuery] string lang)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (lang == "fa")
                ViewBag.PageTitle = "ایجاد نقش";
            else
                ViewBag.PageTitle = "Create user role";

            var request = new PanelMenu();
            return View(GetViewName(lang, "Create"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string lang, PanelMenu request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (string.IsNullOrEmpty(request.Title))
            {
                if (lang == "fa")
                    AddError("نام باید مقدار داشته باشد");
                else
                    AddError("Title can not be null");
            }

            if (Messages.Where(x => x.Type == MessageType.Error).Count() > 0)
            {
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "Create"), request);
            }

            iSystemBaseService.iPanelMenuService.Add(request);
            await iSystemBaseService.iPanelMenuService.SaveChangesAsync();

            return Redirect("/Panel/PanelMenuManage");

        }


        [HttpGet]
        public IActionResult Edit([FromQuery] string lang, int Id)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            var theMenu = iSystemBaseService.iPanelMenuService.Find(x => x.Id == Id);
            return View(GetViewName(lang, "Edit"), theMenu);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromQuery] string lang, PanelMenu request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (request.Id < 1)
            {
                if (lang == "fa")
                    AddError("خطا در ویرایش لطفا از ابتدا عملیات را انجام دهید");
                else
                    AddError("Edit feild, please try agan");
            }

            if (Messages.Where(x => x.Type == MessageType.Error).Count() > 0)
            {
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "Create"), request);
            }

            var theMenu = iSystemBaseService.iPanelMenuService.Find(x => x.Id == request.Id);
            theMenu.Title = request.Title;
            theMenu.Link = request.Link;
            await iSystemBaseService.iPanelMenuService.SaveChangesAsync();

            return Redirect("/Panel/PanelMenuManage");
        }


        public async Task<IActionResult> Remove(int Id)
        {
            var theRole = iSystemBaseService.iPanelMenuService.Find(x => x.Id == Id);
            iSystemBaseService.iPanelMenuService.Remove(theRole);
            await iSystemBaseService.iPanelMenuService.SaveChangesAsync();
            return Redirect("/Panel/PanelMenuManage");
        }


        private string GetViewName(string queryLang, string baseName)
        {
            if (queryLang.ToLower() == "en")
            {
                return baseName;
            }

            return defaultLang.ShortName + baseName;
        }
    }
}
