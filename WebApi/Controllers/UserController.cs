using AutoMapper;
using Infrastructure.UnitOfWork;
using JuiceWorld.Entities;
using JuiceWorld.Enums;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class UserController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "User";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateUser))]
    public async Task<ActionResult<UserDto>> CreateUser(UserDto user)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.UserRepository.Create(mapper.Map<User>(user));
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(mapper.Map<UserDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllUsers))]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.UserRepository.GetAll();
        return Ok(mapper.Map<ICollection<UserDto>>(result).ToList());
    }

    [HttpGet("{userId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetUser))]
    public async Task<ActionResult<UserDto>> GetUser(int userId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.UserRepository.GetById(userId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<UserDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateUser))]
    public async Task<ActionResult<UserDto>> UpdateUser(UserDto user)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.UserRepository.Update(mapper.Map<User>(user));
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(mapper.Map<UserDto>(result));
    }

    [HttpDelete("{userId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteUser))]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.UserRepository.Delete(userId);
        if (!result)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok();
    }
}
