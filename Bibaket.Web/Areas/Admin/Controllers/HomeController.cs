using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
