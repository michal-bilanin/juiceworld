using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using Commons.Utils;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Controllers;

public class UserController(IUserService userService, IAuthService authService, IAuthFacade authFacade, IMapper mapper) : Controller
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

        var token = await authFacade.RegisterAsync(mapper.Map<UserRegisterViewModel, UserRegisterDto>(model));
        if (token is null)
        {
            ModelState.AddModelError("Email", "A user with this username or email already exists.");
            return View(model);
        }

        Response.Cookies.Append(Constants.JwtToken, token, new CookieOptions
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

        Response.Cookies.Append(Constants.JwtToken, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(30) // or set expiration based on token expiry
        });


        return RedirectToAction(nameof(Index), "Home");
    }

    // GET: /User/Logout
    [HttpGet]
    public ActionResult Logout()
    {
        Response.Cookies.Append(Constants.JwtToken, "", new CookieOptions
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
        };

        return View(model);
    }
}
