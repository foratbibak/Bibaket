using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Areas.UserPanel.Controllers
{

    public class HomeController : UserPanelBaseController
    {
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
