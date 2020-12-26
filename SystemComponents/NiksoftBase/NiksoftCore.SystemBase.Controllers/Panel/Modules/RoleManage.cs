using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.ViewModel;
using NiksoftCore.ViewModel.User;
using System.Linq;
using System.Threading.Tasks;

namespace NiksoftCore.SystemBase.Controllers.Panel.Modules
{
    [Area("Panel")]
    [Authorize]
    public class RoleManage : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        private readonly RoleManager<DataModel.Role> roleManager;

        public RoleManage(IConfiguration Configuration,
            UserManager<DataModel.User> userManager,
            RoleManager<DataModel.Role> roleManager) : base(Configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index([FromQuery] string lang)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();

            if (lang == "fa" || defaultLang.ShortName.ToLower() == "fa")
                ViewBag.PageTitle = "مدیریت نقش ها";
            else
                ViewBag.PageTitle = "User role manager";

            ViewBag.Roles = roleManager.Roles.Where(x => true).ToList();
            return View(GetViewName(lang, "Index"));
        }

        [HttpGet]
        public IActionResult Create([FromQuery] string lang)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();

            if (lang == "fa" || defaultLang.ShortName.ToLower() == "fa")
                ViewBag.PageTitle = "ایجاد نقش";
            else
                ViewBag.PageTitle = "Create user role";

            var request = new RoleRequest();
            return View(GetViewName(lang, "Create"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string lang, RoleRequest request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();

            if (string.IsNullOrEmpty(request.Name))
            {
                if (lang == "fa" || defaultLang.ShortName == "fa")
                    AddError("نام نقش کاربری باید مقدار داشته باشد", "fa");
                else
                    AddError("Role name can not be null", "en");

            }

            if (Messages.Any(x => x.Type == MessageType.Error))
            {
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "Create"), request);
            }

            await roleManager.CreateAsync(new DataModel.Role
            {
                Name = request.Name
            });

            return Redirect("/Panel/RoleManage");

        }


        [HttpGet]
        public IActionResult Edit([FromQuery] string lang, int Id)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();

            var theRole = roleManager.Roles.First(x => x.Id == Id);
            var request = new RoleRequest
            {
                Id = theRole.Id,
                Name = theRole.Name,
                NormalizedName = theRole.NormalizedName
            };
            return View(GetViewName(lang, "Edit"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromQuery] string lang, RoleRequest request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();

            if (request.Id < 1)
            {
                if (lang == "fa" || defaultLang.ShortName == "fa")
                    AddError("خطا در ویرایش لطفا از ابتدا عملیات را انجام دهید", "fa");
                else
                    AddError("Edit feild, please try agan", "en");
            }

            if (Messages.Any(x => x.Type == MessageType.Error))
            {
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "Create"), request);
            }

            var theRole = roleManager.Roles.First(x => x.Id == request.Id);
            theRole.Name = request.Name;
            await roleManager.UpdateAsync(theRole);

            return Redirect("/Panel/RoleManage");
        }


        public async Task<IActionResult> Remove(int Id)
        {
            var theRole = roleManager.Roles.First(x => x.Id == Id);
            await roleManager.DeleteAsync(theRole);
            return Redirect("/Panel/RoleManage");
        }


        private string GetViewName(string queryLang, string baseName)
        {
            if (!string.IsNullOrEmpty(queryLang))
            {
                if (queryLang.ToLower() == "en")
                {
                    return baseName;
                }

                var defaultView = ISystemBaseServ.iPortalLanguageServ.Find(x => x.ShortName == queryLang);
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
