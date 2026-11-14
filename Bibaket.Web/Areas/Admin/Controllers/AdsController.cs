using Bibaket.Application.Convertor;
using Bibaket.Application.Generator;
using Bibaket.Domain.Models.Ads;
using Bibaket.Domain.Models.Slider;
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
    public class AdsController : AdminBaseController
    {
        private readonly EshopDbContext _context;

        public AdsController(EshopDbContext context)
        {
            _context = context;
        }

        #region Index
        // GET: Admin/Ads
        public async Task<IActionResult> Index()
        {
            var eshopDbContext = _context.Ads.Include(a => a.AdsZone).OrderBy(s=>s.DeleteDate).ThenBy(s=>s.CreatDate);
            return View(await eshopDbContext.ToListAsync());
        }
        #endregion

        #region Create
        // GET: Admin/Ads/Create
        public IActionResult Create()
        {
            ViewData["ZoneId"] = new SelectList(_context.AdsZones, "Id", "Title");
            return View();
        }

        // POST: Admin/Ads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ads ads, IFormFile? imgUp, string? InputStartdate, string? InputExpireDate)
        {
            if (!string.IsNullOrEmpty(InputStartdate))
            {
                ads.StartDate = DateConvertor.ToMiladi(InputStartdate);
            }
            if (!string.IsNullOrEmpty(InputExpireDate))
            {
                ads.ExpireDate = DateConvertor.ToMiladi(InputExpireDate);
            }
            if (ModelState.IsValid)
            {
                if (imgUp == null)
                {
                    ModelState.AddModelError("ImageName", "لطفا تصویر اسلاید را انتخاب کنید");
                    return View(ads);
                }
                ads.VisitCount = 0;
                ads.CreatDate = DateTime.Now;
                ads.IsDelete = false;
                var imageName = NameGenerator.GenerateUniqName() +
                Path.GetExtension(imgUp.FileName);

                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AdsImages", imageName);
                using (var stream = System.IO.File.Create(savePath))
                {
                    await imgUp.CopyToAsync(stream);
                }
            
                ads.ImageName = imageName;
                _context.Add(ads);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ZoneId"] = new SelectList(_context.AdsZones, "Id", "Title", ads.ZoneId);
            return View(ads);
        }
        #endregion

        #region Edit
        // GET: Admin/Ads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ads = await _context.Ads.FindAsync(id);
            if (ads == null)
            {
                return NotFound();
            }
            ViewData["ZoneId"] = new SelectList(_context.AdsZones, "Id", "Title", ads.ZoneId);
            return View(ads);
        }

        // POST: Admin/Ads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZoneId,Title,Url,ImageName,IBlank,IsActive,StartDate,ExpireDate,VisitCount,Id,CreatDate,UpdateDate,DeleteDate,IsDelete")] Ads ads)
        {
            if (id != ads.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ads);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdsExists(ads.Id))
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
            ViewData["ZoneId"] = new SelectList(_context.AdsZones, "Id", "Title", ads.ZoneId);
            return View(ads);
        }
        #endregion

        #region Delete
        // GET: Admin/Ads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ads = await _context.Ads
                .Include(a => a.AdsZone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ads == null)
            {
                return NotFound();
            }

            return View(ads);
        }

        // POST: Admin/Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ads = await _context.Ads.FindAsync(id);
            if (ads != null)
            {
                _context.Ads.Remove(ads);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        private bool AdsExists(int id)
        {
            return _context.Ads.Any(e => e.Id == id);
        }
    }
}
