using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Constants;
using Commons.Utils;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.Services;

public class AuthService : IAuthService
{
    public string CreateToken(UserDto user)
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

    private static ClaimsIdentity GenerateClaims(UserDto user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        ci.AddClaim(new Claim(ClaimTypes.Role, user.UserRole.ToString()));

        return ci;
    }

    public bool VerifyPassword(UserDto user, string password) =>
        AuthUtils.HashPassword(password, user.PasswordSalt, user.PasswordHashRounds) == user.PasswordHash;
}
