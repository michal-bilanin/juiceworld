using JuiceWorld.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "user")]
public class UserController : ControllerBase
{
    private const string ApiBaseName = "User";
    private readonly JuiceWorldDbContext _dbContext;

    public UserController(JuiceWorldDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateUser))]
    public async Task<ActionResult<bool>> CreateUser(LoginModel userLogin)
    {
        // var user = _dbContext.Users.Add(userLogin);
        var newUser = new User
        {
            UserName = userLogin.UserName,
            PasswordHash = userLogin.Password,
            Email = "test",
            PasswordHashRounds = "test",
            PasswordSalt = "test",
            UserRole = "user",
            Bio = "test"
        };
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();
        var dbUser = await _dbContext.Users.FindAsync(newUser.Id);
        return Ok(dbUser);
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
