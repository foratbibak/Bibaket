using Bibaket.Application.Services.Implementation;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.Models.Categories;
using Bibaket.Domain.Models.Products;
using Bibaket.Domain.ViewModels.Products;
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
    public class ProductsController : AdminBaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryServices _categoryServices;

        public ProductsController(IProductService productService,ICategoryServices categoryServices)
        {
            this._productService = productService;
            this._categoryServices = categoryServices;
        }

        #region Index
        // GET: Admin/Products
        public async Task<IActionResult> Index(AdminFilterProductViewModel model)
        {
            var result=await _productService.ProductFilterAsync(model);
            return View(result);
        }
        #endregion

        #region Deatils
        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View();
        }
        #endregion

        #region Create
        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( AdminCreateProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
        
                return View(product);

            }
            await _productService.CreateProductAsync(product);
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Edit
        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _productService.GetEditProductForAdmin(id);

            var categories =await _categoryServices.GetAllCategoryForMegaMenu();
            int Final_Category = model.CategoryId;

            int sub_Category = categories.First(c => c.Id == Final_Category).ParentId.Value;

            int main_Category = categories.First(c => c.Id == sub_Category).ParentId.Value;

            ViewBag.Final_Category = new SelectList(categories.Where(c=>c.ParentId==sub_Category),"Id", "Title", Final_Category);

            ViewBag.sub_Category = new SelectList(categories.Where(c => c.ParentId == main_Category), "Id", "Title", sub_Category);

            ViewBag.main_Category = new SelectList(categories.Where(c => c.ParentId == null), "Id", "Title", main_Category);


            return View(model);
        }

     
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, AdminEditProductViewModel adminEdit)
        //{
        //    if (id != product.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
              
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View();
        //}
        #endregion

        #region Delete
        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

        

            return View();
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {         
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Tags
        public async Task<JsonResult> GetTags()
        {
            var tags =await _productService.GetTagsAsync();

            return Json(tags.Select(x => new
            {
                id=x.Id,
                value=x.TagTitle,
            }));
        }
        #endregion

    }
}
