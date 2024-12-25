using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Customer))]
public class UserController(IUserService userService) : ControllerBase
{
    private const string ApiBaseName = "User";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateUser))]
    public async Task<ActionResult<UserDto>> CreateUser(UserDto user)
    {
        var result = await userService.CreateUserAsync(user);
        return result == null ? Problem() : Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllUsers))]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var result = await userService.GetAllUsersAsync();
        return Ok(result);
    }

    [HttpGet("{userId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetUser))]
    public async Task<ActionResult<UserDto>> GetUser(int userId)
    {
        var result = await userService.GetUserByIdAsync(userId);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateUser))]
    public async Task<ActionResult<UserDto>> UpdateUser(UserDto user)
    {
        var result = await userService.UpdateUserAsync(user);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{userId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteUser))]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        var result = await userService.DeleteUserByIdAsync(userId);
        return result ? Ok() : NotFound();
    }
}
