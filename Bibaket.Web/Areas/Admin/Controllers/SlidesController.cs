using Bibaket.Application.Convertor;
using Bibaket.Application.Generator;
using Bibaket.Domain.Models.Slider;
using Bibaket.Ifra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibaket.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidesController : AdminBaseController
    {
        private readonly EshopDbContext _context;

        public SlidesController(EshopDbContext context)
        {
            _context = context;
        }

        #region Index
        // GET: Admin/Slides
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }
        #endregion

        #region Deatils
        // GET: Admin/Slides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }
        #endregion

        #region Create
        // GET: Admin/Slides/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Slides/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Slide slide,IFormFile? imgUp ,string? InputStartdate,string? InputExpireDate)
        {
            if (!string.IsNullOrEmpty(InputStartdate))
            {
                slide.StartDate = DateConvertor.ToMiladi(InputStartdate);
            }
            if (!string.IsNullOrEmpty(InputExpireDate))
            {
                slide.ExpireDate = DateConvertor.ToMiladi(InputExpireDate);
            }
            if (ModelState.IsValid)
            {
                if (imgUp == null)
                {
                    ModelState.AddModelError("ImageName", "لطفا تصویر اسلاید را انتخاب کنید");
                    return View(slide);
                }
                slide.CreatDate = DateTime.Now;
                slide.VisitCount = 0;

                var imageName = NameGenerator.GenerateUniqName() +
              Path.GetExtension(imgUp.FileName);

                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Slides", imageName);
                using (var stream = System.IO.File.Create(savePath))
                {
                    await imgUp.CopyToAsync(stream);
                }


                _context.Add(slide);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slide);
        }
        #endregion

        #region Edit
        // GET: Admin/Slides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _context.Sliders.FindAsync(id);
            if (slide == null)
            {
                return NotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slides/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Url,IBlank,ImageName,IsActive,StartDate,ExpireDate,VisitCount,Id,CreatDate,UpdateDate,DeleteDate,IsDelete")] Slide slide)
        {
            if (id != slide.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slide);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlideExists(slide.Id))
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
            return View(slide);
        }

        #endregion

        #region Delete
        // GET: Admin/Slides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }

        // POST: Admin/Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slide = await _context.Sliders.FindAsync(id);
            if (slide != null)
            {
                _context.Sliders.Remove(slide);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        private bool SlideExists(int id)
        {
            return _context.Sliders.Any(e => e.Id == id);
        }
    }
}
