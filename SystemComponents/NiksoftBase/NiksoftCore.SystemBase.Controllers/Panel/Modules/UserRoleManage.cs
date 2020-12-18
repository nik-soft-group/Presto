using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.SystemBase.Service;
using NiksoftCore.ViewModel.SystemBase;
using NiksoftCore.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.SystemBase.Controllers.Panel.Modules
{
    [Area("Panel")]
    [Authorize]
    public class UserRoleManage : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        private readonly RoleManager<DataModel.Role> roleManager;

        public UserRoleManage(IConfiguration Configuration,
            UserManager<DataModel.User> userManager,
            RoleManager<DataModel.Role> roleManager) : base(Configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            iSystemBaseService = new SystemBaseService(Configuration);
        }

        public IActionResult Index([FromQuery] string lang)
        {
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
            if (string.IsNullOrEmpty(request.Name))
            {
                if (lang.ToLower() == "fa" || defaultLang.ShortName == "fa")
                    AddError("نام نقش کاربری باید مقدار داشته باشد");
                else
                    AddError("Role name can not be null");

            }

            if (Messages.Where(x => x.Type == MessageType.Error).Count() > 0)
            {
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "Create"), request);
            }

            await roleManager.CreateAsync(new DataModel.Role { 
                Name = request.Name
            });

            return Redirect("/Panel/UserRoleManage");
            
        }


        [HttpGet]
        public IActionResult Edit([FromQuery] string lang, int Id)
        {
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
            if (request.Id < 1)
            {
                if (lang.ToLower() == "fa" || defaultLang.ShortName == "fa")
                    AddError("خطا در ویرایش لطفا از ابتدا عملیات را انجام دهید");
                else
                    AddError("Edit feild, please try agan");
            }

            if (Messages.Where(x => x.Type == MessageType.Error).Count() > 0)
            {
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "Create"), request);
            }

            var theRole = roleManager.Roles.First(x => x.Id == request.Id);
            theRole.Name = request.Name;
            await roleManager.UpdateAsync(theRole);
            return View(GetViewName(lang, "Edit"), request);
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
