using BusinessLayer.DTOs;
using JuiceWorld.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services.Interfaces;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegisterDto);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> UpdateUserAsync(UserDto userDto);
    Task<bool> DeleteUserByIdAsync(int id);
}
