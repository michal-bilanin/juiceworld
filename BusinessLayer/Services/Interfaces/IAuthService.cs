using System.Security.Claims;
using BusinessLayer.DTOs;

namespace BusinessLayer.Services.Interfaces;

public interface IAuthService
{
    string CreateToken(UserDto user);
    bool VerifyPassword(UserDto user, string password);
}
