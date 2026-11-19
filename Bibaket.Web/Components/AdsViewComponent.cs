using Bibaket.Ifra.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Components
{
    public class AdsViewComponent : ViewComponent
    {
        private readonly EshopDbContext _context;

        public AdsViewComponent(EshopDbContext context)
        {
            this._context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int zoneId)
        {
            DateTime dtstart=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0,0);
            DateTime dtEx=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0,0);
            var data = _context.Ads.Where(s => !s.IsDelete && s.IsActive && s.ZoneId==zoneId);
            data = data.Where(s => s.StartDate == null || s.StartDate <= dtstart);
            data = data.Where(s => s.ExpireDate == null || s.ExpireDate >= dtEx);
            ViewBag.ZoneId = zoneId;
            return View(data);
        }
    }
}
