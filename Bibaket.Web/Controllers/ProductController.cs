using Bibaket.Application.EnCoders;
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

        [Route("Product/{slug}")]
        public async Task<IActionResult> ShowProduct(string slug)
        {
            var product=await _productService.GetProductBySlug(slug);
            return View(product);
        }

        [Route("p/{id}")]

        public async Task<IActionResult> ShortLinkRedirect(int id)
        {
            var slug = await _productService.GetSlugProduct(id);
            var encodedSlug = UrlEncoder.Encoded(slug);
            return RedirectPermanent($"/Product/{slug}");
        }
    
    }
}
