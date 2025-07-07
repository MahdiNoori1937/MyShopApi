using LibraryApi.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryApi.Web.Filters.Permisions;

public class AdminPermissionsAttribute:AuthorizeAttribute,IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        if (context.HttpContext.GetUserRoleTitle() != "Admin")
        {
            context.Result = new ForbidResult();
            return;
        }
        
    }

}