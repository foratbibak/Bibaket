using Bibaket.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bibaket.Web.Components
{
    public class IsCompleteProfileViewComponent : ViewComponent
    {
        private readonly IAccountServices _accountServices;

        public IsCompleteProfileViewComponent(IAccountServices accountServices)
        {
            this._accountServices = accountServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            bool model=await _accountServices.IsCompleteProfile(userId);
            return View(model);    
        }
    }
}
