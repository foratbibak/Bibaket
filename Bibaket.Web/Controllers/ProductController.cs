using Bibaket.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService=productService;
        }
        public async Task<PartialViewResult> ShortDescProduct (int id)
        {
            var product=await _productService.GetProductForShortDescbyIdAsync(id);
            return PartialView(product);
        }
    }
}
