using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bibaket.Domain.Models.Products;
using Bibaket.Ifra.Data.Context;

namespace Bibaket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductFeaturesController : Controller
    {
        private readonly EshopDbContext _context;

        public ProductFeaturesController(EshopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductFeatures
        public async Task<IActionResult> Index()
        {
            var eshopDbContext = _context.ProductFeatures.Include(p => p.Product);
            return View(await eshopDbContext.ToListAsync());
        }

        // GET: Admin/ProductFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFeature = await _context.ProductFeatures
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productFeature == null)
            {
                return NotFound();
            }

            return View(productFeature);
        }

        // GET: Admin/ProductFeatures/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title");
            return View();
        }

        // POST: Admin/ProductFeatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Value,Id,CreatDate,UpdateDate,DeleteDate,IsDelete")] ProductFeature productFeature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title", productFeature.ProductId);
            return View(productFeature);
        }

        // GET: Admin/ProductFeatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFeature = await _context.ProductFeatures.FindAsync(id);
            if (productFeature == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title", productFeature.ProductId);
            return View(productFeature);
        }

        // POST: Admin/ProductFeatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Value,Id,CreatDate,UpdateDate,DeleteDate,IsDelete")] ProductFeature productFeature)
        {
            if (id != productFeature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductFeatureExists(productFeature.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title", productFeature.ProductId);
            return View(productFeature);
        }

        // GET: Admin/ProductFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFeature = await _context.ProductFeatures
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productFeature == null)
            {
                return NotFound();
            }

            return View(productFeature);
        }

        // POST: Admin/ProductFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productFeature = await _context.ProductFeatures.FindAsync(id);
            if (productFeature != null)
            {
                _context.ProductFeatures.Remove(productFeature);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductFeatureExists(int id)
        {
            return _context.ProductFeatures.Any(e => e.Id == id);
        }
    }
}
