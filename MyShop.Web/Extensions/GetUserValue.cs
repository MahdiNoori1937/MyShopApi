using System.Security.Claims;

namespace LibraryApi.Web.Extensions;

public static class GetUserValue
{
    public static int? GetUserId(this HttpContext httpContext)
    {
        return int.Parse(httpContext.User.FindFirstValue("Id"));
    }  
    public static string? GetUserRoleTitle(this HttpContext httpContext)
    {
        return httpContext.User.FindFirstValue("RoleTitle");
    }
    
}