using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdventureWorks2019.Application;

public class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
{
    private readonly string _secretKey = jwtOptions.Value.SecretKey;
    private readonly string _expireMinutes = jwtOptions.Value.ExpireMinutes;

    public  string GenerateToken(UserInfo userInfo)

    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                SecurityAlgorithms.HmacSha256);

        Claim[] claims = [new("user", userInfo.User), new("hashPassword", userInfo.HashPassword)];

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.Now.AddMinutes(Convert.ToInt16(_expireMinutes))
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}