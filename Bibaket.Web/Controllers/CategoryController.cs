using Bibaket.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Controllers
{
    public class CategoryController(IProductService _productService) : Controller
    {
        private readonly IProductService productService = _productService;

        [Route("category/{slug}")]
        public async Task<IActionResult> Index(string slug)
        {
            var products = await _productService.GetCategoryBySlug(slug);
            return View(products);
        }
    }
}
