using Bibaket.Application.Extensions;
using Bibaket.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bibaket.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class PermissionCheckerAttribute(string permissionName) : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var permissionservice = context.HttpContext.RequestServices.GetService<IPermissionService>();
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = context.HttpContext.User.GetUserId();
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

                context.HttpContext.Response.Redirect("/Admin/AccsesDenided");
            }
        }
    }
}
