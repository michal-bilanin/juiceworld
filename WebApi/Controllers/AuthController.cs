using Infrastructure.QueryObjects;
using Infrastructure.UnitOfWork;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using JuiceWorld.Enums;
using JuiceWorld.QueryObjects;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IQueryObject<User> userQueryObject, AuthService authService) : ControllerBase
{
    private const string ApiBaseName = "CartItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(Login))]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login(LoginDto login)
    {
        var user = (await userQueryObject.Filter(user => user.Email == login.Email).Execute()).FirstOrDefault();
        if (user == null)
        {
            return NotFound();
        }

        if (!authService.VerifyPassword(user, login.Password))
        {
            return Unauthorized();
        }

        return Ok(authService.Create(user));
    }
}
