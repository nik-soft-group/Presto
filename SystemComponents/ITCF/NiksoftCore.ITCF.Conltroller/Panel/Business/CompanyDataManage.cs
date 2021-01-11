﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.ITCF.Service;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.Utilities;
using NiksoftCore.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NiksoftCore.ITCF.Conltroller.Panel.Business
{
    [Area("Panel")]
    [Authorize(Roles = "NikAdmin,Admin,Business")]
    public class CompanyDataManage : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        public IITCFService iITCFServ { get; set; }
        private readonly IWebHostEnvironment hosting;

        public CompanyDataManage(IConfiguration Configuration, IWebHostEnvironment hostingEnvironment,
            UserManager<DataModel.User> userManager) : base(Configuration)
        {
            this.userManager = userManager;
            iITCFServ = new ITCFService(Configuration);
            hosting = hostingEnvironment;
        }

        public async Task<IActionResult> Index([FromQuery] string lang, int bid)
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
            var theBusiness = iITCFServ.IBusinessServ.Find(x => x.Id == bid && x.CreatorId == theUser.Id);
            if (theBusiness == null)
            {
                return Redirect("/Panel");
            }

            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            ViewBag.Company = theBusiness;

            if (lang == "fa")
                ViewBag.PageTitle = "مدیریت محتوای کسب و کار";
            else
                ViewBag.PageTitle = "Company Content Management";

            return View(GetViewName(lang, "Index"));
        }


        public async Task<IActionResult> IntroHome([FromQuery] string lang, int bid)
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
            var theBusiness = iITCFServ.IBusinessServ.Find(x => x.Id == bid && x.CreatorId == theUser.Id);
            if (theBusiness == null)
            {
                return Redirect("/Panel");
            }

            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            ViewBag.Company = theBusiness;

            if (lang == "fa")
                ViewBag.PageTitle = "مدیریت محتوای کسب و کار";
            else
                ViewBag.PageTitle = "Company Content Management";

            return View(GetViewName(lang, "IntroHome"));
        }


        public async Task<IActionResult> IntroList([FromQuery] string lang, int bid, int part, int unit)
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
            var theBusiness = iITCFServ.IBusinessServ.Find(x => x.Id == bid && x.CreatorId == theUser.Id);
            if (theBusiness == null)
            {
                return Redirect("/Panel");
            }

            ViewBag.Company = theBusiness;
            ViewBag.Create = "CreatePart" + unit;
            ViewBag.Unit = unit;

            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();



            var keyValue = Tools.GetUnitKey(unit);
            var total = iITCFServ.iIntroductionServ.Count(x => x.BusinessId == bid && x.UserId == theUser.Id && x.KeyValue == keyValue);
            var pager = new Pagination(total, 20, part);
            ViewBag.Pager = pager;

            if (lang == "fa")
                ViewBag.PageTitle = Tools.GetUnitTitle(unit);
            else
                ViewBag.PageTitle = "Company Content Management";

            ViewBag.Contents = iITCFServ.iIntroductionServ.GetPart(x => x.BusinessId == bid && x.UserId == theUser.Id && x.KeyValue == keyValue, pager.StartIndex, pager.PageSize).ToList();

            return View(GetViewName(lang, "IntroList"), new { lang, bid, part, unit });
        }

        [HttpGet]
        public async Task<IActionResult> CreatePart1([FromQuery] string lang, int bid)
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
            var theBusiness = iITCFServ.IBusinessServ.Find(x => x.Id == bid && x.CreatorId == theUser.Id);
            if (theBusiness == null)
            {
                return Redirect("/Panel");
            }

            ViewBag.Company = theBusiness;

            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            var request = new IntroductionRequest();
            request.BusinessId = theBusiness.Id;

            return View(GetViewName(lang, "CreatePart1"), request);

        }

        [HttpPost]
        public async Task<IActionResult> CreatePart1([FromQuery] string lang, [FromForm] IntroductionRequest request)
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
            var theBusiness = iITCFServ.IBusinessServ.Find(x => x.Id == request.BusinessId && x.CreatorId == theUser.Id);
            if (theBusiness == null)
            {
                return Redirect("/Panel");
            }

            ViewBag.Company = theBusiness;

            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (!FormVlideUnit1(lang, request))
            {
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "CreatePart1"), request);
            }

            string Image = string.Empty;
            if (request.Image != null && request.Image.Length > 0)
            {
                var SaveImage = await NikTools.SaveFileAsync(new SaveFileRequest
                {
                    File = request.Image,
                    RootPath = hosting.ContentRootPath,
                    UnitPath = Config.GetSection("FileRoot:BusinessFile").Value
                });

                if (!SaveImage.Success)
                {
                    Messages.Add(new NikMessage
                    {
                        Message = "آپلود فایل انجام نشد مجدد تلاش کنید",
                        Type = MessageType.Error,
                        Language = "Fa"
                    });
                    ViewBag.Messages = Messages;
                    return View(GetViewName(lang, "CreatePart1"), request);
                }

                Image = SaveImage.FilePath;
            }

            var newItem = new Introduction
            {
                Title = request.Title,
                Description = request.Description,
                ImageFile = Image,
                KeyValue = Tools.GetUnitKey(1),
                BusinessId = theBusiness.Id,
                UserId = theUser.Id,
                CreateDate = DateTime.Now
            };

            iITCFServ.iIntroductionServ.Add(newItem);
            await iITCFServ.iIntroductionServ.SaveChangesAsync();
            return Redirect("/Panel/CompanyDataManage/IntroList/?bid=" + request.BusinessId + "&unit=1");

        }

        [HttpGet]
        public async Task<IActionResult> CreatePart2([FromQuery] string lang, int bid)
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
            var theBusiness = iITCFServ.IBusinessServ.Find(x => x.Id == bid && x.CreatorId == theUser.Id);
            if (theBusiness == null)
            {
                return Redirect("/Panel");
            }

            ViewBag.Company = theBusiness;

            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            var request = new IntroductionRequest();
            request.BusinessId = theBusiness.Id;

            return View(GetViewName(lang, "CreatePart2"), request);

        }

        [HttpPost]
        public async Task<IActionResult> CreatePart2([FromQuery] string lang, [FromForm] IntroductionRequest request)
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
            var theBusiness = iITCFServ.IBusinessServ.Find(x => x.Id == request.BusinessId && x.CreatorId == theUser.Id);
            if (theBusiness == null)
            {
                return Redirect("/Panel");
            }

            ViewBag.Company = theBusiness;

            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (!FormVlideUnit1(lang, request))
            {
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "CreatePart2"), request);
            }

            string Image = string.Empty;
            if (request.Image != null && request.Image.Length > 0)
            {
                var SaveImage = await NikTools.SaveFileAsync(new SaveFileRequest
                {
                    File = request.Image,
                    RootPath = hosting.ContentRootPath,
                    UnitPath = Config.GetSection("FileRoot:BusinessFile").Value
                });

                if (!SaveImage.Success)
                {
                    Messages.Add(new NikMessage
                    {
                        Message = "آپلود فایل انجام نشد مجدد تلاش کنید",
                        Type = MessageType.Error,
                        Language = "Fa"
                    });
                    ViewBag.Messages = Messages;
                    return View(GetViewName(lang, "CreatePart2"), request);
                }

                Image = SaveImage.FilePath;
            }

            var newItem = new Introduction
            {
                Title = request.Title,
                Description = request.Description,
                ImageFile = Image,
                KeyValue = Tools.GetUnitKey(2),
                BusinessId = theBusiness.Id,
                UserId = theUser.Id,
                CreateDate = DateTime.Now
            };

            iITCFServ.iIntroductionServ.Add(newItem);
            await iITCFServ.iIntroductionServ.SaveChangesAsync();
            return Redirect("/Panel/CompanyDataManage/IntroList/?bid=" + request.BusinessId + "&unit=2");

        }

        [HttpGet]
        public async Task<IActionResult> CreatePart3([FromQuery] string lang, int bid)
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
            var theBusiness = iITCFServ.IBusinessServ.Find(x => x.Id == bid && x.CreatorId == theUser.Id);
            if (theBusiness == null)
            {
                return Redirect("/Panel");
            }

            ViewBag.Company = theBusiness;

            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            var request = new IntroductionRequest();
            request.BusinessId = theBusiness.Id;

            return View(GetViewName(lang, "CreatePart3"), request);

        }

        [HttpPost]
        public async Task<IActionResult> CreatePart3([FromQuery] string lang, [FromForm] IntroductionRequest request)
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
            var theBusiness = iITCFServ.IBusinessServ.Find(x => x.Id == request.BusinessId && x.CreatorId == theUser.Id);
            if (theBusiness == null)
            {
                return Redirect("/Panel");
            }

            ViewBag.Company = theBusiness;

            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (!FormVlideUnit3(lang, request))
            {
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "CreatePart3"), request);
            }

            string video = string.Empty;
            if (request.Video != null && request.Video.Length > 0)
            {
                var SaveImage = await NikTools.SaveFileAsync(new SaveFileRequest
                {
                    File = request.Video,
                    RootPath = hosting.ContentRootPath,
                    UnitPath = Config.GetSection("FileRoot:BusinessFile").Value
                });

                if (!SaveImage.Success)
                {
                    Messages.Add(new NikMessage
                    {
                        Message = "آپلود فایل انجام نشد مجدد تلاش کنید",
                        Type = MessageType.Error,
                        Language = "Fa"
                    });
                    ViewBag.Messages = Messages;
                    return View(GetViewName(lang, "CreatePart3"), request);
                }

                video = SaveImage.FilePath;
            }

            var newItem = new Introduction
            {
                Title = request.Title,
                Description = request.Description,
                VideoFile = video,
                KeyValue = Tools.GetUnitKey(3),
                BusinessId = theBusiness.Id,
                UserId = theUser.Id,
                CreateDate = DateTime.Now
            };

            iITCFServ.iIntroductionServ.Add(newItem);
            await iITCFServ.iIntroductionServ.SaveChangesAsync();
            return Redirect("/Panel/CompanyDataManage/IntroList/?bid=" + request.BusinessId + "&unit=3");

        }

        public async Task<IActionResult> Remove(int Id, int bid, int unit)
        {
            var theContent = iITCFServ.iIntroductionServ.Find(x => x.Id == Id);
            if (!string.IsNullOrEmpty(theContent.ImageFile))
            {
                NikTools.RemoveFile(new RemoveFileRequest
                {
                    RootPath = hosting.ContentRootPath,
                    FilePath = theContent.ImageFile
                });
            }

            if (!string.IsNullOrEmpty(theContent.VideoFile))
            {
                NikTools.RemoveFile(new RemoveFileRequest
                {
                    RootPath = hosting.ContentRootPath,
                    FilePath = theContent.VideoFile
                });
            }

            if (!string.IsNullOrEmpty(theContent.SoundFile))
            {
                NikTools.RemoveFile(new RemoveFileRequest
                {
                    RootPath = hosting.ContentRootPath,
                    FilePath = theContent.SoundFile
                });
            }

            iITCFServ.iIntroductionServ.Remove(theContent);
            await iITCFServ.iIntroductionServ.SaveChangesAsync();
            return Redirect("/Panel/CompanyDataManage/IntroList/?bid=" + bid + "&unit=" + unit);
        }

        private bool FormVlideUnit1(string lang, IntroductionRequest request)
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

            if (string.IsNullOrEmpty(request.Title))
            {
                if (lang == "fa")
                    AddError("توضیحات باید مقدار داشته باشد", "fa");
                else
                    AddError("Title can not be null", "en");
                result = false;
            }

            if (request.Image == null)
            {
                if (lang == "fa")
                    AddError("تصویر باید مقدار داشته باشد", "fa");
                else
                    AddError("Title can not be null", "en");
                result = false;
            } 
            else if (request.Image.Length > 512000)
            {
                if (lang == "fa")
                    AddError("حجم تصویر نباید بیشتر از 500 KB باشد", "fa");
                else
                    AddError("Title can not be null", "en");
                result = false;
            }

            return result;
        }

        private bool FormVlideUnit3(string lang, IntroductionRequest request)
        {
            bool result = true;
            if (request.Video == null)
            {
                if (lang == "fa")
                    AddError("ویدیو باید مقدار داشته باشد", "fa");
                else
                    AddError("Title can not be null", "en");
                result = false;
            }
            else if (request.Video.FileName.GetExtention() != "mp4")
            {
                if (lang == "fa")
                    AddError("فرمت فایل صحیح نیست", "fa");
                else
                    AddError("Title can not be null", "en");
                result = false;
            }
            else if (request.Video == null && request.Video.Length > 2242880)
            {
                if (lang == "fa")
                    AddError("حجم ویدیو نباید بیشتر از 5 MB باشد", "fa");
                else
                    AddError("Title can not be null", "en");
                result = false;
            }



            return result;
        }

    }
}