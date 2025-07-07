using MyShop.Domain.Common;

namespace MyShop.Domain.Interfaces.IJwtInterface;

public interface IJwtService
{
    Task<string> GetJwtTokenAsync(ClaimSetDto claimSetDto);
}