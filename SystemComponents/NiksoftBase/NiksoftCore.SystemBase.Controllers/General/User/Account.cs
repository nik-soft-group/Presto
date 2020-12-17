using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NiksoftCore.MiddlController.Middles;
using NiksoftCore.SystemBase.Service;
using NiksoftCore.ViewModel.User;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NiksoftCore.SystemBase.Controllers.General.User
{
    [Area("Auth")]
    public class Account : NikController
    {
        private readonly UserManager<DataModel.User> userManager;
        private readonly SignInManager<DataModel.User> signInManager;
        public ISystemBaseService iSystemBaseService { get; set; }

        public Account(UserManager<DataModel.User> userManager, SignInManager<DataModel.User> signInManager, IConfiguration Configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            iSystemBaseService = new SystemBaseService(Configuration);
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register([FromQuery] string lang)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/home");
            }
            UserRegisterRequest model = new UserRegisterRequest();
            return View(GetViewName(lang, "Register"), model);
        }


        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register([FromQuery] string lang, UserRegisterRequest request)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/home");
            }

            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new DataModel.User
                    {
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    var result = await userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(GetViewName(lang, "Register"), request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(GetViewName(lang, "Register"), request);
                }
            }
            return View(GetViewName(lang, "Register"), request);

        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login([FromQuery] string lang)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/home");
            }

            LoginRequest model = new LoginRequest();
            return View(GetViewName(lang, "Login"), model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] string lang, LoginRequest model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/home");
            }

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(GetViewName(lang, "Login"), model);

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(GetViewName(lang, "Login"), model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return Redirect("/Panel");
                }
                else if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(GetViewName(lang, "Login"), model);
                }
            }
            return View(GetViewName(lang, "Login"), model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/Auth/Account/Login");
        }

        private string GetViewName(string queryLang, string baseName)
        {
            var defaultLang = iSystemBaseService.iPortalLanguageServ.Find(x => x.IsDefault);
            if (!string.IsNullOrEmpty(queryLang))
            {
                if (queryLang.ToLower() == "en")
                {
                    return baseName;
                }

                var defaultView = iSystemBaseService.iPortalLanguageServ.Find(x => x.ShortName == queryLang);
                return defaultView.ShortName + baseName;
            }

            if (defaultLang.ShortName.ToLower() == "en")
            {
                return baseName;
            }

            return defaultLang.ShortName + baseName;
        }

    }
}
