using Bibaket.Application.Generator;
using Bibaket.Application.Security;
using Bibaket.Application.Services.Implementation;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.ViewModels.Account;
using Bibaket.Domin.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace Bibaket.Web.Areas.UserPanel.Controllers
{
    public class ProfileController : UserPanelBaseController
    {
        private readonly IAccountServices _accountServices;

        public ProfileController(IAccountServices accountServices)
        {
            this._accountServices = accountServices;
        }

        #region Edit Profile
        public async Task<IActionResult> EditProfile()
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());
            var profile = await _accountServices.GetUserProfile(currentUserId);
            return View(profile);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(ProfileViewModel model,IFormFile? imgAvatar)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (imgAvatar != null)
            {
                if (!imgAvatar.ImageValidate())
                {

                    ViewBag.ValidateImage = false;
                    return View(model);
                }
                if (model.Avatar != "NoPhoto.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars", model.Avatar);
                    if (System.IO.File.Exists(deletePath))
                        System.IO.File.Delete(deletePath);
                }
                string avatarName = NameGenerator.GenerateUniqName() + Path.GetExtension(imgAvatar.FileName);

                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars", avatarName);
                using (var stream = System.IO.File.Create(savePath))
                {
                    imgAvatar.CopyTo(stream);
                }
                model.Avatar = avatarName;
            }
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());
            await _accountServices.EditProfile(currentUserId, model);
            TempData["SuccessEditProfile"] = "True";
            return Redirect("/UserPanel");
        } 
        #endregion

        #region Change Password
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel change)
        {
            if(!ModelState.IsValid)
                return View(change);
            
            var CurrentUserId=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());

            bool result = await _accountServices.ChangePassword(CurrentUserId,change);
            if(!result)
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمیباشد");
                return View(change);
            }
            return Redirect("/LogOut");
        }
        #endregion
    }
}
