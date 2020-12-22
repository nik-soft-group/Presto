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
    public class LegalForm : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        public IITCFService iITCFServ { get; set; }

        public LegalForm(IConfiguration Configuration, UserManager<DataModel.User> userManager) : base(Configuration)
        {
            this.userManager = userManager;
            iITCFServ = new ITCFService(Configuration);
        }

        public IActionResult Index([FromQuery] string lang)
        {
            var thisUser = userManager.GetUserAsync(HttpContext.User);
            var userForm = iITCFServ.IUserLegalFormServ.Find(x => x.UserId == thisUser.Id);
            if (userForm == null)
            {
                userForm = new UserLegalForm();
            }

            ViewBag.userForm = userForm;

            return View(GetViewName(lang, "Index"));
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromQuery] string lang, UserLegalForm request)
        {

            return View();
        }
    }
}
