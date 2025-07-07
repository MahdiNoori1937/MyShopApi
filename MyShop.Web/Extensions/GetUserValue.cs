using System.Security.Claims;

namespace MyShop.Web.Extensions;

public static class GetUserValue
{
    public static int? GetUserId(this HttpContext httpContext)
    {
        return int.Parse(httpContext.User.FindFirstValue("Id")?? "0");
    }  
}