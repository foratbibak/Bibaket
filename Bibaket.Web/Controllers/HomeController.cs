using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("About-Us")]
        public IActionResult AboutUs()
        {
            return View();
        }
        [Route("Contact-Us")]
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }
    }
}
