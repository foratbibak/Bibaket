using Microsoft.AspNetCore.Mvc;

namespace Bibaket.Web.Components
{
    public class UserPanelViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
