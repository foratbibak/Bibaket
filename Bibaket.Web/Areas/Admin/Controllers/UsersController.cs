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

        public UsersController(EshopDbContext context,IUserServices userServices)
        {
            _context = context;
            this._userServices = userServices;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index(string create="false")
        {
            var lst = _context.Users.Select(u => new UserViewModel
            {
                Avatar = u.Avatar,
                CreatDate = u.CreatDate,
                Email = u.Email,
                FirstName = u.FirstName,
                Id = u.Id,
                IsActive = u.IsActive,
                LastName = u.LastName,
                Mobile = u.Mobile,
                NationalCode = u.NationalCode,
                Password = u.Password,
                UpdateDate = u.UpdateDate,
                UserName = u.UserName,
            }).ToList();
            ViewBag.Create = create;
            return View(lst);
        }

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

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            var model = new AdminCreatUserViewModel()
            {
                Roles = _context.Role.ToList()
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
            model.Roles=_context.Role.ToList();
            return View(model);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,UserName,Email,EmailActiveCode,IsEmailActive,Mobile,MobileActiveCode,NationalCode,Password,Avatar,IsActive,LastLoginDate,Id,CreatDate,UpdateDate,DeleteDate,IsDelete")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
