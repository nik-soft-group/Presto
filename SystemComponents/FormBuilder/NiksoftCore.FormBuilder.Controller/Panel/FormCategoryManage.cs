using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NiksoftCore.FormBuilder.Service;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.Utilities;
using NiksoftCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.FormBuilder.Controller.Panel
{
    [Area("Panel")]
    public class FormCategoryManage : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        private readonly IWebHostEnvironment hosting;
        private readonly IFormBuilderService iFormBuilderServ;

        public FormCategoryManage(IConfiguration Configuration, IWebHostEnvironment hostingEnvironment,
            UserManager<DataModel.User> userManager) : base(Configuration)
        {
            this.userManager = userManager;
            hosting = hostingEnvironment;
            iFormBuilderServ = new FormBuilderService(Configuration.GetConnectionString("SystemBase"));
        }

        public IActionResult Index(CategoryListRequest request)
        {
            if (!string.IsNullOrEmpty(request.lang))
                request.lang = request.lang.ToLower();
            else
                request.lang = defaultLang.ShortName.ToLower();

            bool rearchMode = false;
            var query = iFormBuilderServ.iFormCategoryServ.ExpressionMaker();
            query.Add(x => true);
            if (!string.IsNullOrEmpty(request.Title))
            {
                query.Add(x => x.Title.Contains(request.Title));
                rearchMode = true;
            }

            if (request.ParentId > 0)
            {
                query.Add(x => x.ParentId == request.ParentId);
                rearchMode = true;
            }

            ViewBag.Search = rearchMode;

            var total = iFormBuilderServ.iFormCategoryServ.Count(query);
            var pager = new Pagination(total, 20, request.part);
            ViewBag.Pager = pager;

            ViewBag.PageTitle = "Form Categories";

            ViewBag.Contents = iFormBuilderServ.iFormCategoryServ.GetPartOptional(query, pager.StartIndex, pager.PageSize).ToList();
            DropDownBinder(new CategoryRequest { 
                ParentId = request.ParentId
            });
            return View(GetViewName(request.lang, "Index"));
        }

        [HttpGet]
        public IActionResult Create([FromQuery] string lang)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            ViewBag.PageTitle = "Create Category";

            var request = new CategoryRequest();
            DropDownBinder(request);
            return View(GetViewName(lang, "Create"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string lang, [FromForm] CategoryRequest request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            ViewBag.PageTitle = "Create Category";

            if (!FormVlide(request))
            {
                DropDownBinder(request);
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "Create"), request);
            }

            string fileUrl = string.Empty;
            if (request.File != null && request.File.Length > 0)
            {
                var SaveImage = await NikTools.SaveFileAsync(new SaveFileRequest
                {
                    File = request.File,
                    RootPath = hosting.ContentRootPath,
                    UnitPath = Config.GetSection("FileRoot:FormBuilder").Value
                });

                if (!SaveImage.Success)
                {
                    DropDownBinder(request);
                    Messages.Add(new NikMessage
                    {
                        Message = "Upload failed, try agin",
                        Type = MessageType.Error,
                        Language = "Fa"
                    });
                    ViewBag.Messages = Messages;
                    return View(GetViewName(lang, "Create"), request);
                }

                fileUrl = SaveImage.FilePath;
            }

            var newItem = new FormCategory
            {
                Title = request.Title,
                Description = request.Description,
                KeyValue = request.KeyValue,
                Enabled = request.Enabled,
                FileUrl = fileUrl,
                ParentId = request.ParentId > 0 ? request.ParentId : null
            };

            iFormBuilderServ.iFormCategoryServ.Add(newItem);
            await iFormBuilderServ.iFormCategoryServ.SaveChangesAsync();
            return Redirect("/Panel/FormCategoryManage");

        }

        [HttpGet]
        public IActionResult Edit([FromQuery] string lang, int Id)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            ViewBag.PageTitle = "Edit Category";

            var theItem = iFormBuilderServ.iFormCategoryServ.Find(x => x.Id == Id);
            var request = new CategoryRequest
            {
                Id = theItem.Id,
                Title = theItem.Title,
                Description = theItem.Description,
                KeyValue = theItem.KeyValue,
                Enabled = theItem.Enabled,
                FileUrl = theItem.FileUrl,
                ParentId = theItem.ParentId == null ? 0 : theItem.ParentId
            };
            DropDownBinder(request);
            return View(GetViewName(lang, "Edit"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromQuery] string lang, [FromForm] CategoryRequest request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            ViewBag.PageTitle = "Edit Category";

            if (request.Id < 1)
            {
                AddError("Edit failed, please try agin", "en");

            }

            if (!FormVlide(request))
            {
                DropDownBinder(request);
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "Create"), request);
            }

            string fileUrl = string.Empty;
            if (request.File != null && request.File.Length > 0)
            {
                var Image = await NikTools.SaveFileAsync(new SaveFileRequest
                {
                    File = request.File,
                    RootPath = hosting.ContentRootPath,
                    UnitPath = Config.GetSection("FileRoot:FormBuilder").Value
                });

                if (!Image.Success)
                {
                    DropDownBinder(request);
                    Messages.Add(new NikMessage
                    {
                        Message = "Upload failed, try agin",
                        Type = MessageType.Error,
                        Language = "Fa"
                    });
                    ViewBag.Messages = Messages;
                    return View(GetViewName(lang, "Create"), request);
                }

                fileUrl = Image.FilePath;
            }



            var theContent = iFormBuilderServ.iFormCategoryServ.Find(x => x.Id == request.Id);
            theContent.Title = request.Title;
            theContent.Description = request.Description;
            theContent.KeyValue = request.KeyValue;
            if (!string.IsNullOrEmpty(fileUrl))
                theContent.FileUrl = fileUrl;
            theContent.ParentId = request.ParentId > 0 ? request.ParentId : null;
            await iFormBuilderServ.iFormCategoryServ.SaveChangesAsync();

            return Redirect("/Panel/FormCategoryManage");
        }


        public async Task<IActionResult> Remove(int Id)
        {
            var theContent = iFormBuilderServ.iFormCategoryServ.Find(x => x.Id == Id);
            if (!string.IsNullOrEmpty(theContent.FileUrl))
            {
                NikTools.RemoveFile(new RemoveFileRequest
                {
                    RootPath = hosting.ContentRootPath,
                    FilePath = theContent.FileUrl
                });
            }

            iFormBuilderServ.iFormCategoryServ.Remove(theContent);
            await iFormBuilderServ.iFormCategoryServ.SaveChangesAsync();
            return Redirect("/Panel/FormCategoryManage");
        }

        public async Task<IActionResult> Enable(int Id)
        {
            var theContent = iFormBuilderServ.iFormCategoryServ.Find(x => x.Id == Id);
            theContent.Enabled = !theContent.Enabled;
            await iFormBuilderServ.iFormCategoryServ.SaveChangesAsync();
            return Redirect("/Panel/FormCategoryManage");
        }

        private void DropDownBinder(CategoryRequest request)
        {
            var categories = iFormBuilderServ.iFormCategoryServ.GetAll(x => true);
            ViewBag.Parents = new SelectList(categories, "Id", "Title", request?.ParentId);
        }

        private bool FormVlide(CategoryRequest request)
        {
            bool result = true;
            if (string.IsNullOrEmpty(request.Title))
            {
                AddError("Title can not be empty", "en");
                result = false;
            }

            return result;
        }
    }
}
