using Bibaket.Application.Services.Implementation;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Enums.User;
using Bibaket.Domain.ViewModels.User;
using Bibaket.Domin.Models.Users;
using Bibaket.Ifra.Data.Context;
using Bibaket.Ifra.Data.Static;
using Bibaket.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibaket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;

        public UsersController(EshopDbContext context,IUserServices userServices,IRoleServices roleServices)
        {

            this._userServices = userServices;
            this._roleServices = roleServices;
        }

        #region Index
        [PermissionChecker(PermissionName.ManageUsers)]
        public async Task<IActionResult> Index(AdminFilterUserViewModel adminFilter, string create = "false")
        {
            var lst = await _userServices.AdminFilterAsync(adminFilter);
            ViewBag.Create = create;
            return View(lst);
        }
        #endregion



        #region Create
        [PermissionChecker(PermissionName.AddUsers)]
        public async Task<IActionResult> Create()
        {
            var model = new AdminCreatUserViewModel()
            {
                Roles = await _roleServices.GetAllRoleAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.AddUsers)]
        public async Task<IActionResult> Create(AdminCreatUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.CreatUserInAdminAsync(model);
                if (result == AdminCreateUserResult.Success)
                {
                    return Redirect("/Admin/Users?Create=Success");
                }
                else
                {
                    ViewBag.Error = result;
                }
            }
            model.Roles = await _roleServices.GetAllRoleAsync();
            return View(model);
        }
        #endregion

        #region Edit User
        [PermissionChecker(PermissionName.EditUsers)]
        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userServices.GetUserForEditAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.EditUsers)]
        public async Task<IActionResult> Edit(int id, AdminEditViewModel user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            // دیباگ - ببینید چه خطاهایی وجود داره
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            //foreach (var error in errors)
            //{
            //    // اینجا می‌تونید خطا رو لاگ کنید یا با breakpoint ببینید
            //    var errorMessage = error.ErrorMessage;
            //}
            if (!ModelState.IsValid)  
            {
                return View(user);
            }

            var result = await _userServices.EditUserAsync(user);

            if (result == AdminEditUserResult.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Error = result;
            }
            return View(user);
        }
        #endregion

        [PermissionChecker(PermissionName.DeleteUsers)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userServices.GetUserForDeleteAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [PermissionChecker(PermissionName.DeleteUsers)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userServices.DeleteUserAsync(id);

            return RedirectToAction(nameof(Index));
        }

   
    }
}
