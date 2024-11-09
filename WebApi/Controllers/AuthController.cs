using BusinessLayer.DTOs;
using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthFacade authFacade) : ControllerBase
{
    private const string ApiBaseName = "CartItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(Login))]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login(LoginDto login)
    {
        var token = await authFacade.LoginAsync(login);
        return token == null ? Unauthorized() : Ok(token);
    }
}
