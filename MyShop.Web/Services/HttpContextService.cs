using System.Security.Claims;
using MyShop.Application.Common.Interfaces;


namespace MyShop.Web.Services;

public class HttpContextService:IHttpContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public int GetUserId()
    {
        string? userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirstValue("Id");

        if (int.TryParse(userIdStr, out var userId))
            return userId;

        return 0;
    }
}