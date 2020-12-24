using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.ITCF.Service;
using NiksoftCore.MiddlController.Middles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.ITCF.Conltroller.Panel.Forms
{
    [Area("Panel")]
    public class LegalForm : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        public IITCFService iITCFServ { get; set; }

        public LegalForm(IConfiguration Configuration, UserManager<DataModel.User> userManager) : base(Configuration)
        {
            this.userManager = userManager;
            iITCFServ = new ITCFService(Configuration);
        }

        public async Task<IActionResult> Index([FromQuery] string lang)
        {
            ViewBag.PageTitle = "فرم نیازمندی های بازرگانی";
            ViewBag.Title = "فرم نیازمندی های بازرگانی";
            var thisUser = await userManager.GetUserAsync(HttpContext.User);
            var userForm = iITCFServ.IUserLegalFormServ.Find(x => x.UserId == thisUser.Id);
            if (userForm == null)
            {
                userForm = new UserLegalForm();
                userForm.UserId = thisUser.Id;
            }

            return View(GetViewName(lang, "Index"), userForm);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromQuery] string lang, UserLegalForm request)
        {
            if (request.Id == 0)
            {
                iITCFServ.IUserLegalFormServ.Add(request);
                iITCFServ.IUserLegalFormServ.SaveChanges();
            }
            else
            {
                await iITCFServ.IUserLegalFormServ.UpdateAsync(request);
            }
            return View(GetViewName(lang, "Index"), request);
        }
    }
}
