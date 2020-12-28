using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NiksoftCore.ITCF.Service;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.ITCF.Conltroller.Panel.BaseInfo
{
    [Area("Panel")]
    [Authorize]
    public class BusinessCategoryManage : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        public IITCFService iITCFServ { get; set; }

        public BusinessCategoryManage(IConfiguration Configuration, UserManager<DataModel.User> userManager) : base(Configuration)
        {
            this.userManager = userManager;
            iITCFServ = new ITCFService(Configuration);
        }

        public IActionResult Index([FromQuery] string lang, int part)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            var total = iITCFServ.IBusinessCategoryServ.Count(x => true);
            var pager = new Pagination(total, 20, part);
            ViewBag.Pager = pager;



            if (lang == "fa")
                ViewBag.PageTitle = "مدیریت دسته بندی ها";
            else
                ViewBag.PageTitle = "Business Category Management";

            ViewBag.Contents = iITCFServ.IBusinessCategoryServ.GetPart(x => true, pager.StartIndex, pager.PageSize).ToList();

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
                ViewBag.PageTitle = "ایجاد دسته بندی";
            else
                ViewBag.PageTitle = "Create Business Category";

            var request = new BusinessCategory();
            DropDownBinder(request);
            return View(GetViewName(lang, "Create"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string lang, BusinessCategory request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (!FormVlide(lang, request))
            {
                DropDownBinder(request);
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "Create"), request);
            }

            iITCFServ.IBusinessCategoryServ.Add(request);
            await iITCFServ.IBusinessCategoryServ.SaveChangesAsync();
            return Redirect("/Panel/BusinessCategoryManage");

        }

        [HttpGet]
        public IActionResult Edit([FromQuery] string lang, int Id)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (lang == "fa")
                ViewBag.PageTitle = "بروزرسانی دسته بندی";
            else
                ViewBag.PageTitle = "Update Business Category";

            var request = iITCFServ.IBusinessCategoryServ.Find(x => x.Id == Id);
            DropDownBinder(request);
            return View(GetViewName(lang, "Edit"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromQuery] string lang, BusinessCategory request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (request.Id < 1)
            {
                if (lang == "fa")
                    AddError("خطا در ویرایش لطفا از ابتدا عملیات را انجام دهید", "fa");
                else
                    AddError("Edit feild, please try agan", "en");
            }

            if (!FormVlide(lang, request))
            {
                DropDownBinder(request);
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "Create"), request);
            }

            var theContent = iITCFServ.IBusinessCategoryServ.Find(x => x.Id == request.Id);
            theContent.Title = request.Title;
            theContent.Description = request.Description;
            theContent.Icone = request.Icone;
            theContent.Image = request.Image;
            theContent.ParentId = request.ParentId;
            await iITCFServ.IBusinessCategoryServ.SaveChangesAsync();

            return Redirect("/Panel/BusinessCategoryManage");
        }


        public async Task<IActionResult> Remove(int Id)
        {
            var theContent = iITCFServ.IBusinessCategoryServ.Find(x => x.Id == Id);
            iITCFServ.IBusinessCategoryServ.Remove(theContent);
            await iITCFServ.IBusinessCategoryServ.SaveChangesAsync();
            return Redirect("/Panel/BusinessCategoryManage");
        }

        public async Task<IActionResult> Enable(int Id)
        {
            var theContent = iITCFServ.IBusinessCategoryServ.Find(x => x.Id == Id);
            //theContent.Enabled = !theContent.Enabled;
            await iITCFServ.IBusinessCategoryServ.SaveChangesAsync();
            return Redirect("/Panel/IndParkManage");
        }

        private void DropDownBinder(BusinessCategory request)
        {
            var categories = iITCFServ.IBusinessCategoryServ.GetPart(x => true, 0, 200);
            ViewBag.Country = new SelectList(categories, "Id", "Title", request?.ParentId);

            //var provinces = iITCFServ.iProvinceServ.GetPart(x => true, 0, 40);
            //ViewBag.Province = new SelectList(provinces, "Id", "Title", request?.CountryId);

            //var cities = iITCFServ.iProvinceServ.GetPart(x => true, 0, 40);
            //ViewBag.Province = new SelectList(provinces, "Id", "Title", request?.CountryId);
        }

        private bool FormVlide(string lang, BusinessCategory request)
        {
            bool result = true;
            if (string.IsNullOrEmpty(request.Title))
            {
                if (lang == "fa")
                    AddError("عنوان باید مقدار داشته باشد", "fa");
                else
                    AddError("Title can not be null", "en");
                result = false;
            }

            return result;
        }
    }
}
