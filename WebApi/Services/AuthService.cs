using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Constants;

namespace WebApi.Services;

public class AuthService
{
    public string Create(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var secret = Environment.GetEnvironmentVariable(EnvironmentConstants.JwtSecret);

        if (secret == null)
        {
            throw new Exception($"{EnvironmentConstants.JwtSecret} environment variable is not set");
        }

        var privateKey = Encoding.UTF8.GetBytes(secret);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = GenerateClaims(user)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        ci.AddClaim(new Claim(ClaimTypes.Role, user.UserRole));

        return ci;
    }
}
