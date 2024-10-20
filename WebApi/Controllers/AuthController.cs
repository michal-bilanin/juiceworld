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
public class AuthController(JuiceWorldDbContext dbContext, AuthService authService) : ControllerBase
{
    private const string ApiBaseName = "CartItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(Login))]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login(LoginModel login)
    {
        var user = await dbContext.Users.Where(u => u.UserName == login.UserName).FirstOrDefaultAsync();
        if (user == null)
        {
            return NotFound();
        }

        return authService.Create(user);
    }
}
