using JuiceWorld.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using WebApi.Models;
using WebApi.Services;

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

        return _authService.Create(user);
    }
}