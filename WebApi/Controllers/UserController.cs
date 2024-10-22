using Infrastructure.UnitOfWork;
using JuiceWorld.Entities;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "user")]
public class UserController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider) : ControllerBase
{
    private const string ApiBaseName = "User";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateUser))]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.UserRepository.Create(user);
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllUsers))]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.UserRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{userId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetUser))]
    public async Task<ActionResult<User>> GetUser(int userId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.UserRepository.GetById(userId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateUser))]
    public async Task<ActionResult<User>> UpdateUser(User user)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        if (!await unitOfWork.UserRepository.Update(user))
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(user);
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
