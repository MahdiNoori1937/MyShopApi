using LibraryApi.Domain.Common;

namespace LibraryApi.Domain.Interfaces.IJwtInterface;

public interface IJwtService
{
    Task<string> GetJwtTokenAsync(ClaimSetDto claimSetDto);
}