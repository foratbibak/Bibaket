using Bibaket.Application.Services.Implementation;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Enums.User;
using Bibaket.Domain.ViewModels.User;
using Bibaket.Domin.Models.Users;
using Bibaket.Ifra.Data.Context;
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
        private readonly EshopDbContext _context;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;

        public UsersController(EshopDbContext context,IUserServices userServices,IRoleServices roleServices)
        {
            _context = context;
            this._userServices = userServices;
            this._roleServices = roleServices;
        }

        #region Index
        // GET: Admin/Users
        public async Task<IActionResult> Index(string create = "false")
        {
            var lst = await _userServices.ListUsersForAdmin();
            ViewBag.Create = create;
            return View(lst);
        }
        #endregion

        #region Deatiles
        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        #endregion

        #region Create
        // GET: Admin/Users/Create
        public async Task<IActionResult> Create()
        {
            var model = new AdminCreatUserViewModel()
            {
                Roles = await _roleServices.GetAllRoleAsync()
            };
            return View(model);
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Admin/Users/Delete/5
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

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userServices.DeleteUserAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
