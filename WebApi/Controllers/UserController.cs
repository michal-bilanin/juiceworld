using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private const string ApiBaseName = "User";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateUser))]
    public async Task<ActionResult<bool>> CreateUser()
    {
        return Problem();
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllUsers))]
    public async Task<ActionResult<bool>> GetAllUsers()
    {
        return Problem();
    }

    [HttpGet("{userId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetUser))]
    public async Task<ActionResult<bool>> GetUser(int userId)
    {
        return Problem();
    }

    [HttpPut("{userId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(UpdateUser))]
    public async Task<ActionResult<bool>> UpdateUser(int userId)
    {
        return Problem();
    }

    [HttpDelete("{userId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteUser))]
    public async Task<ActionResult<bool>> DeleteUser(int userId)
    {
        return Problem();
    }
}