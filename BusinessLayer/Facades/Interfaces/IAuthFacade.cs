using BusinessLayer.DTOs;

namespace BusinessLayer.Facades.Interfaces;

public interface IAuthFacade
{
    /// <summary>
    /// Attempts a login with the given email and password.
    /// </summary>
    /// <returns>Auth token if successful, otherwise null.</returns>
    Task<string?> LoginAsync(LoginDto login);
    
    Task<string?> RegisterAsync(UserRegisterDto userRegisterDto);
}
