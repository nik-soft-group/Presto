using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NiksoftCore.ITCF.Service;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace NiksoftCore.ITCF.Conltroller.Panel.Business
{
    [Area("Panel")]
    public class AdminBusinessManage : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        public IITCFService iITCFServ { get; set; }
        private readonly IWebHostEnvironment hosting;

        public AdminBusinessManage(IConfiguration Configuration, IWebHostEnvironment hostingEnvironment,
            UserManager<DataModel.User> userManager) : base(Configuration)
        {
            this.userManager = userManager;
            iITCFServ = new ITCFService(Configuration);
            hosting = hostingEnvironment;
        }

        public async Task<IActionResult> Index([FromQuery] string lang, int part)
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            var total = iITCFServ.IBusinessServ.Count(x => true);
            var pager = new Pagination(total, 20, part);
            ViewBag.Pager = pager;

            if (lang == "fa")
                ViewBag.PageTitle = "مدیریت کسب و کار";
            else
                ViewBag.PageTitle = "Business Management";

            ViewBag.Contents = iITCFServ.IBusinessServ.GetPart(x => true, pager.StartIndex, pager.PageSize).ToList();

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
                ViewBag.PageTitle = "ایجاد کسب و کار";
            else
                ViewBag.PageTitle = "Create Business Category";

            var request = new BusinessRequest();
            request.ProvinceId = 0;
            request.IndustrialParkId = 0;
            request.CatgoryId = 0;
            request.CountryId = 1;
            DropDownBinder(request);
            return View(GetViewName(lang, "Create"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string lang, [FromForm] BusinessRequest request)
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
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

            var newCat = new Service.Business
            {
                CoName = request.CoName,
                Tel = request.Tel,
                Email = request.Email,
                Website = request.Website,
                BusinessType = request.BusinessType,
                CountryId = request.CountryId,
                ProvinceId = request.ProvinceId,
                CityId = request.CityId,
                Address = request.Address,
                Location = request.Location,
                IndustrialParkId = request.IndustrialParkId,
                CatgoryId = request.CatgoryId,
                CreatorId = theUser.Id,
                Status = BusinessStatus.RegisterRequest
            };

            iITCFServ.IBusinessServ.Add(newCat);
            await iITCFServ.IBusinessServ.SaveChangesAsync();
            return Redirect("/Panel/BusinessManage");

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

            var item = iITCFServ.IBusinessServ.Find(x => x.Id == Id);
            var request = new BusinessRequest
            {
                Id = item.Id,
                CoName = item.CoName,
                Tel = item.Tel,
                Email = item.Email,
                Website = item.Website,
                BusinessType = item.BusinessType,
                CountryId = item.CountryId,
                ProvinceId = item.ProvinceId,
                CityId = item.CityId,
                Address = item.Address,
                Location = item.Location,
                IndustrialParkId = item.IndustrialParkId,
                CatgoryId = item.CatgoryId
            };
            DropDownBinder(request);
            return View(GetViewName(lang, "Edit"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromQuery] string lang, [FromForm] BusinessRequest request)
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



            var theContent = iITCFServ.IBusinessServ.Find(x => x.Id == request.Id);
            theContent.CoName = request.CoName;
            theContent.Tel = request.Tel;
            theContent.Email = request.Email;
            theContent.Website = request.Website;
            theContent.BusinessType = request.BusinessType;
            theContent.CountryId = request.CountryId;
            theContent.ProvinceId = request.ProvinceId;
            theContent.CityId = request.CityId;
            theContent.Address = request.Address;
            theContent.Location = request.Location;
            theContent.IndustrialParkId = request.IndustrialParkId;
            await iITCFServ.IBusinessServ.SaveChangesAsync();

            return Redirect("/Panel/BusinessManage");
        }


        public async Task<IActionResult> Remove(int Id)
        {
            var theContent = iITCFServ.IBusinessServ.Find(x => x.Id == Id);
            iITCFServ.IBusinessServ.Remove(theContent);
            await iITCFServ.IBusinessServ.SaveChangesAsync();
            return Redirect("/Panel/BusinessManage");
        }

        public async Task<IActionResult> Confirm(int Id)
        {
            var theContent = iITCFServ.IBusinessServ.Find(x => x.Id == Id);
            if (theContent.Status == BusinessStatus.RegisterRequest)
            {
                theContent.Status = BusinessStatus.RegisterConfirme;
            }
            else
            {
                theContent.Status = BusinessStatus.RegisterRequest;
            }

            await iITCFServ.IBusinessServ.SaveChangesAsync();
            return Redirect("/Panel/AdminBusinessManage");
        }

        private void DropDownBinder(BusinessRequest request)
        {
            var countries = iITCFServ.iCountryServ.GetAll(x => true);
            ViewBag.Country = new SelectList(countries, "Id", "Title", request?.CountryId);

            var IndustrialParks = iITCFServ.IIndustrialParkServ.GetAll(x => true);
            ViewBag.Parks = new SelectList(IndustrialParks, "Id", "Title", request?.IndustrialParkId);

            var categories = iITCFServ.IBusinessCategoryServ.GetAll(x => true);
            ViewBag.Categories = new SelectList(categories, "Id", "Title", request?.CatgoryId);
        }

        private bool FormVlide(string lang, BusinessRequest request)
        {
            bool result = true;
            if (string.IsNullOrEmpty(request.CoName))
            {
                if (lang == "fa")
                    AddError("نام شرکت/تولیدی باید مقدار داشته باشد", "fa");
                else
                    AddError("Company name can not be null", "en");
                result = false;
            }

            if (string.IsNullOrEmpty(request.Tel))
            {
                if (lang == "fa")
                    AddError("شماره تماس باید مقدار داشته باشد", "fa");
                else
                    AddError("Company Tel can not be null", "en");
                result = false;
            }

            if (request.CountryId == 0)
            {
                if (lang == "fa")
                    AddError("کشور باید مقدار داشته باشد", "fa");
                else
                    AddError("Country can not be null", "en");
                result = false;
            }

            if (request.CountryId == 1 && (request.ProvinceId == null || request.ProvinceId == 0))
            {
                if (lang == "fa")
                    AddError("استان باید مقدار داشته باشد", "fa");
                else
                    AddError("Province can not be null", "en");
                result = false;
            }

            if (request.CityId == 0)
            {
                if (lang == "fa")
                    AddError("شهر باید مقدار داشته باشد", "fa");
                else
                    AddError("City can not be null", "en");
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

            if (request.CatgoryId == 0)
            {
                if (lang == "fa")
                    AddError("دسته بندی باید مقدار داشته باشد", "fa");
                else
                    AddError("Category can not be null", "en");
                result = false;
            }

            return result;
        }
    }
}
