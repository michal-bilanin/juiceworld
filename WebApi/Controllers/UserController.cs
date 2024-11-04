using AutoMapper;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using JuiceWorld.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class UserController(IRepository<User> userRepository, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "User";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateUser))]
    public async Task<ActionResult<UserDto>> CreateUser(UserDto user)
    {
        var result = await userRepository.CreateAsync(mapper.Map<User>(user));
        return result == null ? Problem() : Ok(mapper.Map<UserDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllUsers))]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        var result = await userRepository.GetAllAsync();
        return Ok(mapper.Map<ICollection<UserDto>>(result).ToList());
    }

    [HttpGet("{userId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetUser))]
    public async Task<ActionResult<UserDto>> GetUser(int userId)
    {
        var result = await userRepository.GetByIdAsync(userId);
        return result == null ? NotFound() : Ok(mapper.Map<UserDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateUser))]
    public async Task<ActionResult<UserDto>> UpdateUser(UserDto user)
    {
        var result = await userRepository.UpdateAsync(mapper.Map<User>(user));
        return result == null ? Problem() : Ok(mapper.Map<UserDto>(result));
    }

    [HttpDelete("{userId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteUser))]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        var result = await userRepository.DeleteAsync(userId);
        return result ? Ok() : NotFound();
    }
}
