using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bibaket.Ifra.Data.Context;
using Sofarashel.Domain.Models.Products;

namespace Bibaket.Web.Areas.Admin.Controllers
{
    public class ProductColorsController : AdminBaseController
    {
        private readonly EshopDbContext _context;

        public ProductColorsController(EshopDbContext context)
        {
            _context = context;
        }

        #region Index
        // GET: Admin/ProductColors
        public async Task<IActionResult> Index(int id)
        {
            var eshopDbContext = _context.ProductColors.Include(p => p.Product);
            ViewBag.ProductTitle = _context.Products.FirstOrDefault(p => p.Id == id)?.Title;

            return View(await eshopDbContext.ToListAsync());
        }
        #endregion


        #region Create
        // GET: Admin/ProductColors/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Code,Price,IsDefault,Quntity,Id,CreatDate,UpdateDate,DeleteDate,IsDelete")] ProductColor productColor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title", productColor.ProductId);
            return View(productColor);
        }
        #endregion

        #region Edit
        // GET: Admin/ProductColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productColor = await _context.ProductColors.FindAsync(id);
            if (productColor == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title", productColor.ProductId);
            return View(productColor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Code,Price,IsDefault,Quntity,Id,CreatDate,UpdateDate,DeleteDate,IsDelete")] ProductColor productColor)
        {
            if (id != productColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductColorExists(productColor.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title", productColor.ProductId);
            return View(productColor);
        }
        #endregion

        #region Delete
        // GET: Admin/ProductColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productColor = await _context.ProductColors
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productColor == null)
            {
                return NotFound();
            }

            return View(productColor);
        }

        // POST: Admin/ProductColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productColor = await _context.ProductColors.FindAsync(id);
            if (productColor != null)
            {
                _context.ProductColors.Remove(productColor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        public IActionResult IsDefault(int id)
        {
            var productcolor = _context.ProductColors.Find(id);
            var colors = _context.ProductColors.Where(c => c.ProductId == productcolor.ProductId);
            foreach (var color in colors)
            {
                color.IsDefault = false;
            }
            productcolor.IsDefault = true;
            _context.SaveChanges();
            return RedirectToAction("Index",new {id=productcolor.ProductId});
        }

        private bool ProductColorExists(int id)
        {
            return _context.ProductColors.Any(e => e.Id == id);
        }
    }
}
