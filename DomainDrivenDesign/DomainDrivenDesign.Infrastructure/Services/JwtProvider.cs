using DomainDrivenDesign.Application.Services;
using DomainDrivenDesign.Domain.Users;
using DomainDrivenDesign.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DomainDrivenDesign.Infrastructure.Services;
internal sealed class JwtProvider(IOptions<Jwt> options) : IJWtProvider
{
    public string CreateToken(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim("UserName",user.UserName!),
            new Claim("Email",user.Email!)
        };

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(options.Value.SecretKey!));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(jwtSecurityToken);

        return token;
    }
}
