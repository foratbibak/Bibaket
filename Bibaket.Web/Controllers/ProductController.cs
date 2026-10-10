using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Controllers
{
    public class ProductController : Controller
    {
        public PartialViewResult ShortDescProduct (int id)
        {
            return PartialView();
        }
    }
}
