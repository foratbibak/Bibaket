using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }



        [Route("/Admin/AccsesDenided")]
        public IActionResult AccsessDenied()
        {
            return View("AccsessDenied");
        }
    }
}
