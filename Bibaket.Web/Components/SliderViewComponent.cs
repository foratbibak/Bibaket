using Bibaket.Ifra.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Components
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly EshopDbContext _context;

        public SliderViewComponent(EshopDbContext context)
        {
            this._context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            DateTime dtstart=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0,0);
            DateTime dtEx=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0,0);
            var data = _context.Sliders.Where(s => !s.IsDelete && s.IsActive);
            data = data.Where(s => s.StartDate == null || s.StartDate <= dtstart);
            data = data.Where(s => s.ExpireDate == null || s.ExpireDate >= dtEx);
            return View(data);
        }
    }
}
