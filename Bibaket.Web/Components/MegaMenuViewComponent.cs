using Bibaket.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Components
{
    public class MegaMenuViewComponent(ICategoryServices categoryServices) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data =await categoryServices.GetAllCategoryForMegaMenu();

            return View(data);
        }
    }
}
