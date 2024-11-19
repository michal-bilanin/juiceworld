using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services.Interfaces;

public interface IUserService
{
    Task<UserDto?> CreateUserAsync(UserDto userDto);
    Task<string?> RegisterUserAsync(UserRegisterDto userRegisterDto);
    public Task<string?> LoginAsync(LoginDto login);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> UpdateUserAsync(UserDto userDto);
    Task<bool> DeleteUserByIdAsync(int id);
}
