using Bibaket.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<PartialViewResult> ShortDescProduct(int id)
        {
            var product = await _productService.GetProductForShortDescbyIdAsync(id);
            return PartialView(product);
        }

        [Route("Product/Product{id}.html")]
        public async Task<IActionResult> ShowProduct(int Id)
        {
            var product=await _productService.GetProductForProductPage(Id);
            return View(product);
        }
    
    }
}
