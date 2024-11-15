using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using Commons.Utils;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Controllers;

public class UserController(IUserService userService, IAuthService authService, IAuthFacade authFacade) : Controller
{
    // GET: /User/Register
    [HttpGet]
    public ActionResult Register()
    {
        return View();
    }

    // POST: /User/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(UserRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (await userService.GetUserByEmailAsync(model.Email) is not null)
        {
            ModelState.AddModelError("Email", "A user with this username or email already exists.");
            return View(model);
        }

        var salt = AuthUtils.GenerateSalt();
        var userDto = new UserDto
        {
            UserName = model.Username,
            Email = model.Email,
            PasswordSalt = salt,
            PasswordHash = AuthUtils.HashPassword(model.Password, salt, 10),
            PasswordHashRounds = 10,
            Bio = model.Bio,
        };
        
        await userService.CreateUserAsync(userDto);
        var token = authService.CreateToken(userDto);
        
        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(30) // or set expiration based on token expiry
        });
        
        return RedirectToAction("Index", "Home");
    }
    //GET: /User/Login
    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }
    
    // POST: /User/Login
    [HttpPost]
    public async Task<ActionResult> Login(UserLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("InvalidCredentials", "Invalid username or password.");
            return View(model);
        }

        var token = await authFacade.LoginAsync(new LoginDto
        {
            Email = model.Email,
            Password = model.Password,
        });
        
        if (token is null)
        {
            ModelState.AddModelError("InvalidCredentials", "Invalid username or password.");
            return View(model);
        }
        
        Response.Cookies.Append("AuthToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(30) // or set expiration based on token expiry
        });
        
        
        return RedirectToAction("Index", "Home");
    }
    
    // GET: /User/Logout
    [HttpGet]
    public ActionResult Logout()
    {
        Response.Cookies.Append("AuthToken", "", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddDays(-1) // Expire immediately
        });
        return RedirectToAction("Login", "User");
    }
    
    // GET: /User/Profile
    [HttpGet]
    public async Task<ActionResult> Profile()
    {
        var user = await userService.GetUserByEmailAsync(User.FindFirst(ClaimTypes.Email)?.Value ?? "");
        var model = new UserProfileViewModel
        {
            Username = user.UserName,
            Email = user.Email,
            Bio = user.Bio,
            ProfileImageUrl = "",
        };
        
        return View(model);
    }
}
