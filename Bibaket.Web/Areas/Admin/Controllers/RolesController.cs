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

        public RolesController(EshopDbContext context, IRoleServices roleServices)
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
                permissions = await _roleServices.GetAllPermissionAsync()
            };
            return View(adminCreateRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateRoleViewModel adminCrate)
        {
            if (ModelState.IsValid)
            {
                await _roleServices.CreateRole(adminCrate);
                return RedirectToAction(nameof(Index));
            }
            adminCrate.permissions = await _roleServices.GetAllPermissionAsync();
            return View(adminCrate);
        }

        // GET: Admin/Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminEditRoleViewModel adminEdit = new AdminEditRoleViewModel();
            adminEdit.permissions = await _roleServices.GetAllPermissionAsync();
            var role = await _roleServices.GetRoleByIdForAdmin(id);
            adminEdit.RoleId=role.Id;
            if (role.RolePermissionMappings.Any())
            {
                adminEdit.PermissonSelectedIds = role.RolePermissionMappings.Select(r => r.PermissionId).ToList();
            }
            adminEdit.RoleName = role.RoleName;
            return View(adminEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdminEditRoleViewModel role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _roleServices.EditRoleAsync(role);
                return RedirectToAction(nameof(Index));
            }
            role.permissions = await _roleServices.GetAllPermissionAsync();
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
