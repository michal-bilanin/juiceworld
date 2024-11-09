using BusinessLayer.DTOs;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;

namespace BusinessLayer.Facades;

public class AuthFacade(IUserService userService, IAuthService authService) : IAuthFacade
{
    public async Task<string?> LoginAsync(LoginDto login)
    {
        var user = await userService.GetUserByEmailAsync(login.Email);
        if (user is null)
        {
            return null;
        }

        return !authService.VerifyPassword(user, login.Password) ? null : authService.CreateToken(user);
    }
}
