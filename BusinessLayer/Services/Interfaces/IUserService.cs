using BusinessLayer.DTOs;
using Commons.Enums;
using Infrastructure.QueryObjects;

namespace BusinessLayer.Services.Interfaces;

public interface IUserService
{
    Task<UserDto?> CreateUserAsync(UserDto userDto);
    Task<string?> RegisterUserAsync(UserRegisterDto userRegisterDto, UserRole role);
    Task<string?> RegisterUserAsync(UserRegisterDto userRegisterDto);
    public Task<string?> LoginAsync(LoginDto login);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<FilteredResult<UserDto>> GetUsersFilteredAsync(UserFilterDto userFilter);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> UpdateUserAsync(UserDto userDto);
    Task<UserDto?> UpdateUserAsync(UserUpdateDto userDto);
    Task<bool> DeleteUserByIdAsync(int id);
}
