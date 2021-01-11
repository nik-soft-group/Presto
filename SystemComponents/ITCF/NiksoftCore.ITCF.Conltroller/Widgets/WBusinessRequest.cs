using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.DataModel;
using NiksoftCore.ITCF.Service;
using System.Threading.Tasks;

namespace NiksoftCore.ITCF.Conltroller.Panel.Widgets
{
    public class WBusinessRequest : ViewComponent
    {
        private readonly UserManager<DataModel.User> userManager;
        public IITCFService IITCFServ { get; set; }

        public WBusinessRequest(IConfiguration Configuration, UserManager<DataModel.User> userManager)
        {
            this.userManager = userManager;
            IITCFServ = new ITCFService(Configuration);
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var theUser = await userManager.GetUserAsync(HttpContext.User);
            var theRoles = await userManager.IsInRoleAsync(theUser, "User");
            ViewBag.AllowView = false;
            ViewBag.BusinessCount = 0;
            if (theRoles)
            {
                ViewBag.AllowView = true;
                ViewBag.BusinessCount = IITCFServ.IBusinessServ.Count(x => true);
            }
            return View();
        }

    }
}
