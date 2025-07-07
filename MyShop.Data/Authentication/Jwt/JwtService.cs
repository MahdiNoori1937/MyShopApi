using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyShop.Domain.Common;
using MyShop.Domain.Interfaces.IJwtInterface;

namespace MyShop.Data.Authentication.Jwt;

public class JwtService:IJwtService
{
    private readonly SymmetricSecurityKey _key;
    private readonly MyApiSecurityKey Security;

    public JwtService(IOptions<MyApiSecurityKey> _Security)
    {
        Security = _Security.Value;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Security.SecurityKey));

    }
    public async  Task<string> GetJwtTokenAsync(ClaimSetDto claimSetDto)
    {
        List<Claim> claims = new()
        {
            new Claim("Id", claimSetDto.Id.ToString()),
        };
        
        SigningCredentials credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: "https://localhost:7151",
            audience: "https://localhost:7151",
            claims: claims,
            expires: DateTime.Now.AddDays(20),
            signingCredentials: credentials
        );
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        return  jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
    }
}