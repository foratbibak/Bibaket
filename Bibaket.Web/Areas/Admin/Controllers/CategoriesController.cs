using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Models.Categories;
using Bibaket.Domain.ViewModels.Categories;
using Bibaket.Ifra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Bibaket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly EshopDbContext _context;
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(EshopDbContext context, ICategoryServices categoryServices)
        {
            _context = context;
            _categoryServices = categoryServices;
        }

        #region Index
        // GET: Admin/Categories
        public async Task<IActionResult> Index(CategoryFilterViewModel filter)
        {
            var result = await _categoryServices.FilterAsync(filter);
            return View(result);
        }
        #endregion

        #region SubCategory
        public async Task<IActionResult> SubCategory(int id)
        {
            var filter = new CategoryFilterViewModel()
            {
                ParentId = id
            };

            var result = await _categoryServices.FilterAsync(filter);
            return View("Index", result);
        }
        #endregion

        #region Create Category
        // GET: Admin/Categories/Create
        public async Task<IActionResult> Create(int? id)
        {
            AdminCreateCategoryViewModel categoryViewModel = new AdminCreateCategoryViewModel()
            {
                ParentId = id
            };

            if (id != null)
            {
                var categoryparent = await _categoryServices.GetCategoryById(id.Value);
                categoryViewModel.CategoryParentTitle = categoryparent.Title;
            }

            return View(categoryViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateCategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _categoryServices.IsExistSlug(categoryViewModel.Slug))
                {
                    ModelState.AddModelError("Slug", "این آدرس بار  وجورد دارد");
                }
                await _categoryServices.CreateCategoryAsync(categoryViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryViewModel);
        }
        #endregion

        #region Edit Category
        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AdminEditCategoryViewModel adminedit = new AdminEditCategoryViewModel();
            var category = await _categoryServices.GetCategoryById(id.Value);
            if (category.ParentId != null)
            {
                var categoryparent = await _categoryServices.GetCategoryById(category.ParentId.Value);
                adminedit.CategoryParentTitle = categoryparent.Title;
            }
            adminedit.Slug = category.Slug;
            adminedit.Title = category.Title;
            adminedit.CategoryId = id.Value;
            adminedit.ParentId = category.ParentId;
            adminedit.ImageName = category.ImageName;
            return View(adminedit);
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdminEditCategoryViewModel category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoryServices.EditCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        #endregion

        #region Delete Category
        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryServices.GetCategoryById(id);
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryServices.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        public async Task<JsonResult> GetCategories(int? parentId)
        {
            var categories=await _categoryServices.GetCategoriesAsync(parentId);

            return Json(categories);
        }

    }
}
