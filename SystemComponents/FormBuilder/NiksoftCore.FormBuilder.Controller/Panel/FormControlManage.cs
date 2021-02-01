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
    public class FormControlManage : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        private readonly IWebHostEnvironment hosting;
        private readonly IFormBuilderService iFormBuilderServ;

        public FormControlManage(IConfiguration Configuration, IWebHostEnvironment hostingEnvironment,
            UserManager<DataModel.User> userManager) : base(Configuration)
        {
            this.userManager = userManager;
            hosting = hostingEnvironment;
            iFormBuilderServ = new FormBuilderService(Configuration.GetConnectionString("SystemBase"));
        }

        public async Task<IActionResult> Index(ControlListRequest request)
        {
            if (!string.IsNullOrEmpty(request.lang))
                request.lang = request.lang.ToLower();
            else
                request.lang = defaultLang.ShortName.ToLower();

            var theFrom = await iFormBuilderServ.iFormServ.FindAsync(x => x.Id == request.FormId);

            ViewBag.PageTitle = theFrom.Title;
            ViewBag.Form = theFrom;
            ViewBag.Controls = iFormBuilderServ.iFormControlServ.GetAll(x => x.FormId == request.FormId).ToList();

            return View(GetViewName(request.lang, "Index"));
        }

        [HttpGet]
        public IActionResult AddControl(string lang, int Id, int row, int col, int formId)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            ViewBag.PageTitle = "Create Form";

            var request = new FormControlRequest();

            if (Id > 0)
            {
                var item = iFormBuilderServ.iFormControlServ.Find(x => x.Id == Id);
                request.Label = item.Label;
                request.FormId = item.FormId;
                request.KeyValue = item.KeyValue;
                request.Description = item.Description;
                request.Size = item.Size;
                request.ControlType = item.ControlType;
                request.ReferenceId = item.ReferenceId;
                request.ListControlId = item.ListControlId == null ? 0 : item.ListControlId.Value;
            }
            else
            {
                request.FormId = formId;
                request.ControlType = ControlType.TextBox;
                request.Size = 3;
            }

            request.RowId = row;
            request.ColumnId = col;




            DropDownBinder(request);

            return View(GetViewName(lang, "AddControl"), request);
        }

        [HttpPost]
        public async Task<IActionResult> AddControl([FromQuery] string lang, [FromForm] FormControlRequest request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (!FormVlide(request))
            {
                ViewBag.Messages = Messages;
                DropDownBinder(request);
                return View(GetViewName(lang, "AddControl"), request);
            }

            FormControl item;
            if (request.Id > 0)
            {
                item = iFormBuilderServ.iFormControlServ.Find(x => x.Id == request.Id);
            }
            else
            {
                item = new FormControl();
            }

            item.RowId = request.RowId;
            item.ColumnId = request.ColumnId;
            item.FormId = request.FormId;
            item.Label = request.Label;
            item.KeyValue = request.KeyValue;
            item.Description = request.Description;
            item.Size = request.Size;
            item.ControlType = request.ControlType;
            item.ReferenceId = request.ReferenceId;
            item.ListControlId = request.ListControlId == 0 ? null : request.ListControlId;

            if (request.Id == 0)
            {
                iFormBuilderServ.iFormControlServ.Add(item);
            }

            await iFormBuilderServ.iFormControlServ.SaveChangesAsync();
            return Redirect("/Panel/FormControlManage?FormId=" + request.FormId);

        }


        public async Task<IActionResult> RemoveControl(int Id)
        {
            var theContent = iFormBuilderServ.iFormControlServ.Find(x => x.Id == Id);
            int formId = theContent.FormId;
            iFormBuilderServ.iFormControlServ.Remove(theContent);
            await iFormBuilderServ.iFormControlServ.SaveChangesAsync();
            return Redirect("/Panel/FormControlManage?FormId=" + formId);
        }

        public async Task<IActionResult> EnableControl(int Id)
        {
            var theContent = iFormBuilderServ.iFormServ.Find(x => x.Id == Id);
            theContent.Enabled = !theContent.Enabled;
            await iFormBuilderServ.iFormServ.SaveChangesAsync();
            return Redirect("/Panel/FormBuilder");
        }


        private void DropDownBinder(FormControlRequest request)
        {
            //var categories = iFormBuilderServ.iListItemServ.GetAll(x => x.ControlId == request.Id);
            //ViewBag.Categories = new SelectList(categories, "Id", "Title", 0);
            var controlTypes = Enum.GetValues(typeof(ControlType)).Cast<ControlType>().ToList();
            var readyList = new List<ListItem>();
            foreach (var item in controlTypes)
            {
                readyList.Add(new ListItem
                {
                    Id = Convert.ToInt32(item),
                    Title = item.GetControlName()
                });
            }
            ViewBag.ControlTypes = new SelectList(readyList, "Id", "Title", (int)request.ControlType);

            var listControls = iFormBuilderServ.iListControlServ.GetAll(x => true);
            ViewBag.ListControls = new SelectList(listControls, "Id", "Title", request.ListControlId);
        }

        private bool FormVlide(FormControlRequest request)
        {
            bool result = true;
            if (string.IsNullOrEmpty(request.Label))
            {
                AddError("Label can not be empty", "en");
                result = false;
            }



            return result;
        }

        //--------------------------------list control-------------------------------
        [HttpGet]
        public IActionResult ListControls(ListControlGridRequest request)
        {
            if (!string.IsNullOrEmpty(request.lang))
                request.lang = request.lang.ToLower();
            else
                request.lang = defaultLang.ShortName.ToLower();

            bool rearchMode = false;

            var query = iFormBuilderServ.iListControlServ.ExpressionMaker();
            query.Add(x => true);
            if (!string.IsNullOrEmpty(request.Title))
            {
                query.Add(x => x.Title.Contains(request.Title));
                rearchMode = true;
            }

            ViewBag.PageTitle = "List Of Contents";

            ViewBag.Search = rearchMode;

            var total = iFormBuilderServ.iListControlServ.Count(query);
            var pager = new Pagination(total, 20, request.part);
            ViewBag.Pager = pager;

            ViewBag.Contents = iFormBuilderServ.iListControlServ.GetPartOptional(query, pager.StartIndex, pager.PageSize).ToList();
            return View(GetViewName(request.lang, "ListControls"));
        }

        [HttpGet]
        public IActionResult ListControlCreate([FromQuery] string lang, int Id)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            ViewBag.PageTitle = "Create Form";
            var request = new ListControlRequest();
            if (Id > 0)
            {
                var item = iFormBuilderServ.iListControlServ.Find(x => x.Id == Id);
                request.Id = item.Id;
                request.Title = item.Title;
                request.Description = item.Description;
                request.ParentId = item.ParentId != null ? item.ParentId.Value : 0;
            }
            else
            {
                request.ParentId = 0;
            }



            return View(GetViewName(lang, "ListControlCreate"), request);
        }

        [HttpPost]
        public async Task<IActionResult> ListControlCreate([FromQuery] string lang, [FromForm] ListControlRequest request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (!ValidListControl(request))
            {
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "ListItemCreate"), request);
            }

            ListControl item;

            if (request.Id > 0)
            {
                item = iFormBuilderServ.iListControlServ.Find(x => x.Id == request.Id);
            }
            else
            {
                item = new ListControl();
                
            }

            item.Title = request.Title;
            item.Description = request.Description;
            item.ParentId = request.ParentId == 0 ? null : request.ParentId;

            if (request.Id == 0)
            {
                iFormBuilderServ.iListControlServ.Add(item);
            }

            await iFormBuilderServ.iListControlServ.SaveChangesAsync();
            return Redirect("/Panel/FormControlManage/ListControls");

        }

        private bool ValidListControl(ListControlRequest request)
        {
            bool result = true;
            if (string.IsNullOrEmpty(request.Title))
            {
                AddError("Title can not be empty", "en");
                result = false;
            }



            return result;
        }


        //--------------------------------list items-------------------------------


        [HttpGet]
        public IActionResult ListItems(ListItemRequest request)
        {
            if (!string.IsNullOrEmpty(request.lang))
                request.lang = request.lang.ToLower();
            else
                request.lang = defaultLang.ShortName.ToLower();

            var theList = iFormBuilderServ.iListControlServ.Find(x => x.Id == request.ListControlId);
            ViewBag.ListControl = theList;

            bool rearchMode = false;

            var query = iFormBuilderServ.iListItemServ.ExpressionMaker();
            query.Add(x => x.ListControlId == request.ListControlId);
            if (!string.IsNullOrEmpty(request.Title))
            {
                query.Add(x => x.Title.Contains(request.Title));
                rearchMode = true;
            }

            ViewBag.Search = rearchMode;

            var total = iFormBuilderServ.iListItemServ.Count(query);
            var pager = new Pagination(total, 20, request.part);
            ViewBag.Pager = pager;

            ViewBag.PageTitle = theList.Title;

            ViewBag.Contents = iFormBuilderServ.iListItemServ.GetPartOptional(query, pager.StartIndex, pager.PageSize).ToList();
            return View(GetViewName(request.lang, "ListItems"));
        }

        [HttpGet]
        public IActionResult ListItemCreate([FromQuery] string lang, int Id, int ListControlId)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            ViewBag.PageTitle = "Create List Item";
            var request = new ItemRequest();
            if (Id > 0)
            {
                var item = iFormBuilderServ.iListItemServ.Find(x => x.Id == Id);
                request.Id = item.Id;
                request.Title = item.Title;
                request.KeyValue = item.KeyValue;
                request.OrderId = item.OrderId;
                request.Enabled = item.Enabled;
                request.ListControlId = item.ListControlId;
            }
            else
            {
                request.ListControlId = ListControlId;
            }



            return View(GetViewName(lang, "ListItemCreate"), request);
        }

        [HttpPost]
        public async Task<IActionResult> ListItemCreate([FromQuery] string lang, [FromForm] ItemRequest request)
        {
            if (!string.IsNullOrEmpty(lang))
                lang = lang.ToLower();
            else
                lang = defaultLang.ShortName.ToLower();

            if (!ValidListItem(request))
            {
                ViewBag.Messages = Messages;
                return View(GetViewName(lang, "ListItemCreate"), request);
            }

            ListItem item;

            if (request.Id > 0)
            {
                item = iFormBuilderServ.iListItemServ.Find(x => x.Id == request.Id);
            }
            else
            {
                item = new ListItem();
                var max = iFormBuilderServ.iListItemServ.Count(x => x.ListControlId == request.ListControlId);
                item.OrderId = max + 1;
                item.Enabled = true;
                item.ListControlId = request.ListControlId;
            }

            item.Title = request.Title;
            item.KeyValue = request.KeyValue;

            if (request.Id == 0)
            {
                iFormBuilderServ.iListItemServ.Add(item);
            }

            await iFormBuilderServ.iFormServ.SaveChangesAsync();
            return Redirect("/Panel/FormControlManage/ListItems?ListControlId=" + request.ListControlId);

        }

        private bool ValidListItem(ItemRequest request)
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
