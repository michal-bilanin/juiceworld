using JuiceWorld.Data;
using jwtAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private const string ApiBaseName = "CartItem";
    private readonly AuthService _authService;

    private readonly JuiceWorldDbContext _dbContext;

    public AuthController(JuiceWorldDbContext dbContext, AuthService authService)
    {
        _dbContext = dbContext;
        _authService = authService;
    }

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(Login))]
    [AllowAnonymous]
    public async Task<ActionResult<String>> Login(LoginModel login)
    {
        var user = await _dbContext.Users.Where(u => u.UserName == login.UserName).FirstAsync();
        if (user == null)
        {
            return NotFound();
        }

        // if (user.PasswordHash != login.Password)
        // {
        //     return Unauthorized();
        // }

        return _authService.Create(user);
    }
}