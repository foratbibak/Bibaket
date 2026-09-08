using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bibaket.Domain.Models.Products;
using Bibaket.Ifra.Data.Context;
using Bibaket.Application.Services.Interfaces;
using Bibaket.Domain.ViewModels.Products;

namespace Bibaket.Web.Areas.Admin.Controllers
{
    public class ProductsController : AdminBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }

        #region Index
        // GET: Admin/Products
        public async Task<IActionResult> Index(AdminFilterProductViewModel model)
        {
            var result=await _productService.ProductFilterAsync(model);
            return View();
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Title,Price,ShortDescription,Review,DeatilReview,ImageName,Count,IsActive,Id,CreatDate,UpdateDate,DeleteDate,IsDelete")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
              
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
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

    }
}
