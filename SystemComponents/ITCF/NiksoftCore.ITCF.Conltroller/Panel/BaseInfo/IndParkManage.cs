using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NiksoftCore.ITCF.Service;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.Utilities;
using NiksoftCore.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiksoftCore.ITCF.Conltroller.Panel.BaseInfo
{
    [Area("Panel")]
    public class IndParkManage : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        public IITCFService iITCFServ { get; set; }

        public IndParkManage(IConfiguration Configuration, UserManager<DataModel.User> userManager) : base(Configuration)
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

            var total = iITCFServ.IIndustrialParkServ.Count(x => true);
            var pager = new Pagination(total, 20, part);
            ViewBag.Pager = pager;

            

            if (lang == "fa")
                ViewBag.PageTitle = "مدیریت شهرک صنعتی";
            else
                ViewBag.PageTitle = "Industrial Park Management";

            ViewBag.Contents = iITCFServ.IIndustrialParkServ.GetPart(x => true, pager.StartIndex, pager.PageSize).ToList();

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
                ViewBag.PageTitle = "ایجاد شهرک صنعتی";
            else
                ViewBag.PageTitle = "Create Industrial Park";

            var request = new IndustrialPark();
            request.ProvinceId = 0;
            DropDownBinder(request);
            return View(GetViewName(lang, "Create"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string lang, IndustrialPark request)
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

            iITCFServ.IIndustrialParkServ.Add(request);
            await iITCFServ.IIndustrialParkServ.SaveChangesAsync();
            return Redirect("/Panel/IndParkManage");

        }

        [HttpGet]
        public IActionResult Edit([FromQuery] string lang, int Id)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (lang == "fa")
                ViewBag.PageTitle = "بروزرسانی شهرک صنعتی";
            else
                ViewBag.PageTitle = "Update Industrial Park";

            var request = iITCFServ.IIndustrialParkServ.Find(x => x.Id == Id);
            DropDownBinder(request);
            return View(GetViewName(lang, "Edit"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromQuery] string lang, IndustrialPark request)
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

            var theContent = iITCFServ.IIndustrialParkServ.Find(x => x.Id == request.Id);
            theContent.Title = request.Title;
            theContent.CountryId = request.CountryId;
            theContent.ProvinceId = request.ProvinceId;
            theContent.CityId = request.CityId;
            theContent.Address = request.Address;
            theContent.Location = request.Location;
            await iITCFServ.IIndustrialParkServ.SaveChangesAsync();

            return Redirect("/Panel/IndParkManage");
        }


        public async Task<IActionResult> Remove(int Id)
        {
            var theMenu = iITCFServ.IIndustrialParkServ.Find(x => x.Id == Id);
            iITCFServ.IIndustrialParkServ.Remove(theMenu);
            await iITCFServ.IIndustrialParkServ.SaveChangesAsync();
            return Redirect("/Panel/IndParkManage");
        }

        public async Task<IActionResult> Enable(int Id)
        {
            var theMenu = iITCFServ.IIndustrialParkServ.Find(x => x.Id == Id);
            //theMenu.Enabled = !theMenu.Enabled;
            await iITCFServ.IIndustrialParkServ.SaveChangesAsync();
            return Redirect("/Panel/IndParkManage");
        }

        private void DropDownBinder(IndustrialPark request)
        {
            var countries = iITCFServ.iCountryServ.GetPart(x => true, 0, 200);
            ViewBag.Country = new SelectList(countries, "Id", "Title", request?.CountryId);

            //var provinces = iITCFServ.iProvinceServ.GetPart(x => true, 0, 40);
            //ViewBag.Province = new SelectList(provinces, "Id", "Title", request?.CountryId);

            //var cities = iITCFServ.iProvinceServ.GetPart(x => true, 0, 40);
            //ViewBag.Province = new SelectList(provinces, "Id", "Title", request?.CountryId);
        }

        private bool FormVlide(string lang, IndustrialPark request)
        {
            bool result = true;
            if (string.IsNullOrEmpty(request.Title))
            {
                if (lang == "fa")
                    AddError("نام باید مقدار داشته باشد", "fa");
                else
                    AddError("Title can not be null", "en");
                result = false;
            }

            if (request.CountryId == 0)
            {
                if (lang == "fa")
                    AddError("کشور را انتخاب نمایید", "fa");
                else
                    AddError("Choose the country", "en");
                result = false;
            }

            if (request.ProvinceId == 0)
            {
                if (lang == "fa")
                    AddError("استان را انتخاب نمایید", "fa");
                else
                    AddError("Choose the province", "en");
                result = false;
            }

            if (request.CityId == 0)
            {
                if (lang == "fa")
                    AddError("شهر را انتخاب نمایید", "fa");
                else
                    AddError("Choose the city", "en");
                result = false;
            }

            if (string.IsNullOrEmpty(request.Address))
            {
                if (lang == "fa")
                    AddError("آدرس باید مقدار داشته باشد", "fa");
                else
                    AddError("Address can not be null", "en");
                result = false;
            }

            return result;
        }
    }
}
