using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using NiksoftCore.FormBuilder.Service;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.Utilities;
using NiksoftCore.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace NiksoftCore.FormBuilder.Controller.Panel
{
    [Area("Panel")]
    public class FormBuilder : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        private readonly IWebHostEnvironment hosting;
        private readonly IFormBuilderService iFormBuilderServ;

        public FormBuilder(IConfiguration Configuration, IWebHostEnvironment hostingEnvironment,
            UserManager<DataModel.User> userManager) : base(Configuration)
        {
            this.userManager = userManager;
            hosting = hostingEnvironment;
            iFormBuilderServ = new FormBuilderService(Configuration.GetConnectionString("SystemBase"));
        }

        public IActionResult Index(FormListRequest request)
        {
            if (!string.IsNullOrEmpty(request.lang))
                request.lang = request.lang.ToLower();
            else
                request.lang = defaultLang.ShortName.ToLower();

            bool rearchMode = false;

            var query = iFormBuilderServ.iFormServ.ExpressionMaker();
            query.Add(x => true);
            if (!string.IsNullOrEmpty(request.Title))
            {
                query.Add(x => x.Title.Contains(request.Title));
                rearchMode = true;
            }

            if (request.CategoryId > 0)
            {
                query.Add(x => x.CategoryId == request.CategoryId);
                rearchMode = true;
            }

            ViewBag.Search = rearchMode;

            var total = iFormBuilderServ.iFormServ.Count(query);
            var pager = new Pagination(total, 20, request.part);
            ViewBag.Pager = pager;

            ViewBag.PageTitle = "Forms";

            ViewBag.Contents = iFormBuilderServ.iFormServ.GetPartOptional(query, pager.StartIndex, pager.PageSize).ToList();
            DropDownBinder(new FormRequest {
                CategoryId = request.CategoryId
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

            ViewBag.PageTitle = "Create Form";

            var request = new FormRequest();
            DropDownBinder(request);
            return View(GetViewName(lang, "Create"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string lang, [FromForm] FormRequest request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (!FormVlide(request))
            {
                DropDownBinder(request);
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "Create"), request);
            }

            string fileUrl = string.Empty;
            if (request.FormFile != null && request.FormFile.Length > 0)
            {
                var SaveImage = await NikTools.SaveFileAsync(new SaveFileRequest
                {
                    File = request.FormFile,
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

            var newItem = new Form
            {
                Title = request.Title,
                Description = request.Description,
                KeyValue = request.KeyValue,
                Rows = request.Rows,
                Columns = request.Columns,
                IsPublic = request.IsPublic,
                Enabled = request.Enabled,
                IsSavedIP = request.IsSavedIP,
                IsShare = request.IsShare,
                FileUrl = fileUrl,
                ReferenceId = request.ReferenceId,
                CategoryId = request.CategoryId
            };

            iFormBuilderServ.iFormServ.Add(newItem);
            await iFormBuilderServ.iFormServ.SaveChangesAsync();
            return Redirect("/Panel/FormBuilder");

        }

        [HttpGet]
        public IActionResult Edit([FromQuery] string lang, int Id)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            ViewBag.PageTitle = "Edit Form";

            var theItem = iFormBuilderServ.iFormServ.Find(x => x.Id == Id);
            var request = new FormRequest
            {
                Id = theItem.Id,
                Title = theItem.Title,
                Description = theItem.Description,
                KeyValue = theItem.KeyValue,
                Rows = theItem.Rows,
                Columns = theItem.Columns,
                IsPublic = theItem.IsPublic,
                Enabled = theItem.Enabled,
                IsSavedIP = theItem.IsSavedIP,
                IsShare = theItem.IsShare,
                FileUrl = theItem.FileUrl,
                ReferenceId = theItem.ReferenceId,
                CategoryId = theItem.CategoryId
            };
            DropDownBinder(request);
            return View(GetViewName(lang, "Edit"), request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromQuery] string lang, [FromForm] FormRequest request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

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
            if (request.FormFile != null && request.FormFile.Length > 0)
            {
                var Image = await NikTools.SaveFileAsync(new SaveFileRequest
                {
                    File = request.FormFile,
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



            var theContent = iFormBuilderServ.iFormServ.Find(x => x.Id == request.Id);
            theContent.Title = request.Title;
            theContent.Description = request.Description;
            theContent.KeyValue = request.KeyValue;
            theContent.Rows = request.Rows;
            theContent.Columns = request.Columns;
            theContent.IsPublic = request.IsPublic;
            theContent.Enabled = request.Enabled;
            theContent.IsSavedIP = request.IsSavedIP;
            theContent.IsShare = request.IsShare;
            if (!string.IsNullOrEmpty(fileUrl))
                theContent.FileUrl = fileUrl;
            theContent.CategoryId = request.CategoryId;
            await iFormBuilderServ.iFormServ.SaveChangesAsync();

            return Redirect("/Panel/FormBuilder");
        }


        public async Task<IActionResult> Remove(int Id)
        {
            var theContent = iFormBuilderServ.iFormServ.Find(x => x.Id == Id);
            if (!string.IsNullOrEmpty(theContent.FileUrl))
            {
                NikTools.RemoveFile(new RemoveFileRequest
                {
                    RootPath = hosting.ContentRootPath,
                    FilePath = theContent.FileUrl
                });
            }

            iFormBuilderServ.iFormServ.Remove(theContent);
            await iFormBuilderServ.iFormServ.SaveChangesAsync();
            return Redirect("/Panel/FormBuilder");
        }

        public async Task<IActionResult> Enable(int Id)
        {
            var theContent = iFormBuilderServ.iFormServ.Find(x => x.Id == Id);
            theContent.Enabled = !theContent.Enabled;
            await iFormBuilderServ.iFormServ.SaveChangesAsync();
            return Redirect("/Panel/FormBuilder");
        }

        public async Task<IActionResult> PrivacyAction(int Id)
        {
            var theContent = iFormBuilderServ.iFormServ.Find(x => x.Id == Id);
            theContent.IsPublic = !theContent.IsPublic;
            await iFormBuilderServ.iFormServ.SaveChangesAsync();
            return Redirect("/Panel/FormBuilder");
        }

        public async Task<IActionResult> SavedIP(int Id)
        {
            var theContent = iFormBuilderServ.iFormServ.Find(x => x.Id == Id);
            theContent.IsSavedIP = !theContent.IsSavedIP;
            await iFormBuilderServ.iFormServ.SaveChangesAsync();
            return Redirect("/Panel/FormBuilder");
        }

        public async Task<IActionResult> Shared(int Id)
        {
            var theContent = iFormBuilderServ.iFormServ.Find(x => x.Id == Id);
            theContent.IsShare = !theContent.IsShare;
            await iFormBuilderServ.iFormServ.SaveChangesAsync();
            return Redirect("/Panel/FormBuilder");
        }

        private void DropDownBinder(FormRequest request)
        {
            var categories = iFormBuilderServ.iFormCategoryServ.GetAll(x => true);
            ViewBag.Categories = new SelectList(categories, "Id", "Title", request?.CategoryId);
        }

        private bool FormVlide(FormRequest request)
        {
            bool result = true;
            if (string.IsNullOrEmpty(request.Title))
            {
                AddError("Title can not be empty", "en");
                result = false;
            }

            if (request.Rows == 0)
            {
                AddError("Rows can not be 0", "en");
                result = false;
            }

            if (request.Columns == 0)
            {
                AddError("Columns can not be 0", "en");
                result = false;
            }

            if (request.CategoryId == 0)
            {
                AddError("Category can not be empty", "en");
                result = false;
            }

            return result;
        }
    }
}
