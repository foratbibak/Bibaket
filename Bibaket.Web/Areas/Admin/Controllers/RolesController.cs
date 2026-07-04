using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bibaket.Domain.Models.Roles;
using Bibaket.Ifra.Data.Context;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.ViewModels.Roles;

namespace Bibaket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly EshopDbContext _context;
        private readonly IRoleServices _roleServices;

        public RolesController(EshopDbContext context,IRoleServices roleServices)
        {
            _context = context;
            this._roleServices = roleServices;
        }

        // GET: Admin/Roles
        public async Task<IActionResult> Index()
        {
            return View(await _roleServices.GetAllRoleAsync());
        }



        // GET: Admin/Roles/Create
        public async Task<IActionResult> Create()
        {

            AdminCreateRoleViewModel adminCreateRole = new AdminCreateRoleViewModel
            {
               permissions =await _roleServices.GetAllPermissionAsync()
            };
            return View(adminCreateRole);
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateRoleViewModel adminCrate)
        {
            if (ModelState.IsValid)
            {
                await _roleServices.CreateRole(adminCrate);
                return RedirectToAction(nameof(Index));
            }
            adminCrate.permissions=await _roleServices.GetAllPermissionAsync();
            return View(adminCrate);
        }

        // GET: Admin/Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Role.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleName,Id,CreatDate,UpdateDate,DeleteDate,IsDelete")] Role role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.Id))
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
            return View(role);
        }

        // GET: Admin/Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Role
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _context.Role.FindAsync(id);
            if (role != null)
            {
                _context.Role.Remove(role);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
            return _context.Role.Any(e => e.Id == id);
        }
    }
}
