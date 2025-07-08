using MyShop.Application.Common.Interfaces;
using MyShop.Web.Extensions;

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
       return int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
    }
}