using Bibaket.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Components
{
    public class TopProductsViewComponent(IProductService productService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await productService.GetTopProductForshowAsync();

            return View(data);
        }
    }
}
