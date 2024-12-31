using BusinessLayer.DTOs;
using JuiceWorld.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Commons.Enums;
using Infrastructure.QueryObjects;

namespace BusinessLayer.Services.Interfaces;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegisterDto, UserRole role = UserRole.Customer);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<FilteredResult<UserDto>> GetUsersFilteredAsync(UserFilterDto userFilter);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> UpdateUserAsync(UserUpdateDto userDto);
    Task<bool> DeleteUserByIdAsync(int id);
}
