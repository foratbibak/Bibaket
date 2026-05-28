using ASPSnippets.Core.Captcha;
using Bibaket.Application.Services.Implementation;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domin.Enums;
using Bibaket.Domin.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Bibaket.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;

        #region Create Captcha
        public Captcha Captcha
        {
            get
            {
                return JsonConvert.DeserializeObject<Captcha>(TempData["Captcha"].ToString());
            }
            set
            {
                TempData["Captcha"] = JsonConvert.SerializeObject(value);
            }
        }
        #endregion

        #region Canstructor
        public AccountController(IAccountServices accountServices)
        {
            this._accountServices = accountServices;
        }
        #endregion

        #region Rigester
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("Register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            var result = await _accountServices.RegisterAsync(register);
            switch (result)
            {
                case RegisterUserResult.Success:
                    return View("~/Views/Account/SuccessRegister.cshtml", register);
                case RegisterUserResult.EmailDuplicated:
                    ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است");
                    break;
                case RegisterUserResult.UserNameDuplicated:
                    ModelState.AddModelError("UserName", "نام کاربری وارد شده تکراری است");
                    break;
                case RegisterUserResult.SendActivationEmail:
                    ModelState.AddModelError("Email", "ارسال ایمیل فعال سازی با مشکل مواجه شد، لطفا دقایقی دیگر تلاش کنید");
                    break;
                case RegisterUserResult.InValidInputs:
                    ModelState.AddModelError("", "لطفا قیلد های روی فرم را پر کنید");
                    break;
                case RegisterUserResult.Failed:
                    ModelState.AddModelError("", "خطای ناشناخته ، لطفا به پشتیبانی اطلاع دهید");
                    break;

            }
            return View(register);

        }
        #endregion

        #region Activation Email
        [Route("VerifyEmail/{activeCode}")]
        public async Task<IActionResult> ActiveAccount(string activeCode)
        {
            ViewBag.ActiveAccount = await _accountServices.ActiveAccountAsync(activeCode);
            return View();
        }

        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login(string ReturnUrl = "/")
        {
            ViewBag.ReturnUrl = ReturnUrl;
            this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D",
            Mode.Numeric);
            ViewBag.ReturnUrl = ReturnUrl;
            return View(new LoginViewModel()
            {
                ImageData = this.Captcha.ImageData
            });
            return View();
        }

        [HttpPost("Login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            if (!Captcha.IsValid(login.CaptchaAnswer))
            {
                this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D",
                  Mode.Numeric);
                ModelState.AddModelError("CaptchaAnswer", "عبارت امنیتی صحیح نیست");
                login.ImageData = Captcha.ImageData;
                return View(login);
            }

            var result = await _accountServices.LoginUserAsync(login);


            if (result == LoginUserResult.NotFound)
            {
                ModelState.AddModelError("", "اطلاعات وارد شده صحیح نمی باشد");
                this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D",
                Mode.Numeric);
                login.ImageData = Captcha.ImageData;
                return View(login);
            }

            if (result == LoginUserResult.NotActive)
            {
                ViewBag.UserNotActive = true;
                return View(login);
            }

            var user = await _accountServices.GetUserByEmailOrUserName(login.UserNameOrEmail);
            var claims = new List<Claim>() {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim("FullName",$"{user.FirstName} {user.LastName}"),
            new Claim("Mobile",user.Mobile??""),
            new Claim("Avatar",user.Avatar)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };
            await HttpContext.SignInAsync(principal, properties);
            if (Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            return View("/");

        }
        #endregion

        #region LogOut
        [Route("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion
    }
}
