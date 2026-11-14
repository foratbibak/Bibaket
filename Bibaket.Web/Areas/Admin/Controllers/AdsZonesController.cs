using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bibaket.Domain.Models.Ads;
using Bibaket.Ifra.Data.Context;

namespace Bibaket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdsZonesController : AdminBaseController
    {
        private readonly EshopDbContext _context;

        public AdsZonesController(EshopDbContext context)
        {
            _context = context;
        }

        #region Index
        // GET: Admin/AdsZones
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdsZones.ToListAsync());
        }
        #endregion


        #region Create

        // GET: Admin/AdsZones/Create
        public IActionResult Create()
        {
    
            return View();
        }

        // POST: Admin/AdsZones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] AdsZone adsZone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adsZone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adsZone);
        }
        #endregion

        #region Edit
        // GET: Admin/AdsZones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adsZone = await _context.AdsZones.FindAsync(id);
            if (adsZone == null)
            {
                return NotFound();
            }
            return View(adsZone);
        }

        // POST: Admin/AdsZones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] AdsZone adsZone)
        {
            if (id != adsZone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adsZone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdsZoneExists(adsZone.Id))
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
            return View(adsZone);
        }
        #endregion

        #region Delete
        // GET: Admin/AdsZones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adsZone = await _context.AdsZones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adsZone == null)
            {
                return NotFound();
            }

            return View(adsZone);
        }

        // POST: Admin/AdsZones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adsZone = await _context.AdsZones.FindAsync(id);
            if (adsZone != null)
            {
                _context.AdsZones.Remove(adsZone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        private bool AdsZoneExists(int id)
        {
            return _context.AdsZones.Any(e => e.Id == id);
        }
    }
}
