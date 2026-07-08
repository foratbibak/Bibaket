using Microsoft.AspNetCore.Mvc.Filters;

namespace Bibaket.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class PermissionCheckerAttribute(string permissionName) : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {

            }
            //else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

                context.HttpContext.Response.Redirect("/Admin/AccsesDenided");
            }
        }
    }
}
