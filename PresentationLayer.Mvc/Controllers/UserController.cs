using System.Security.Claims;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using JuiceWorld.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Controllers;

public class UserController(IUserService userService,
    IMapper mapper, 
    UserManager<LocalIdentityUser> userManager,
    SignInManager<LocalIdentityUser> signInManager) : Controller
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
        
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            Bio = model.Bio,
        };
        
        var userIdentity = new LocalIdentityUser
        {
            User = user,
            UserName = model.Email,
            Email = model.Email
        };
        
        var result = await userManager.CreateAsync(userIdentity, model.Password);

        if (result.Succeeded)
        {
            await signInManager.SignInAsync(userIdentity, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        // var token = await userService.RegisterUserAsync(mapper.Map<UserRegisterViewModel, UserRegisterDto>(model));
        // if (token is null)
        // {
        //     ModelState.AddModelError("Email", "A user with this username or email already exists.");
        //     return View(model);
        // }
        //
        // Response.Cookies.Append(Constants.JwtToken, token, new CookieOptions
        // {
        //     HttpOnly = true,
        //     Secure = true,
        //     Expires = DateTimeOffset.UtcNow.AddMinutes(30) // or set expiration based on token expiry
        // });

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
        
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

        if (result.Succeeded)
        {
            // return RedirectToAction("LoginSuccess", "Account");
            return RedirectToAction(nameof(Index), "Home");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        

        // var token = await userService.LoginAsync(new LoginDto
        // {
        //     Email = model.Email,
        //     Password = model.Password,
        // });
        //
        // if (token is null)
        // {
        //     ModelState.AddModelError("InvalidCredentials", "Invalid username or password.");
        //     return View(model);
        // }
        //
        // Response.Cookies.Append(Constants.JwtToken, token, new CookieOptions
        // {
        //     HttpOnly = true,
        //     Secure = true,
        //     Expires = DateTimeOffset.UtcNow.AddMinutes(30) // or set expiration based on token expiry
        // });


        return RedirectToAction(nameof(Index), "Home");
    }

    // GET: /User/Logout
    [HttpGet]
    public ActionResult Logout()
    {
        signInManager.SignOutAsync();
        return RedirectToAction("Login", "User");
    }

    // GET: /User/Profile
    [HttpGet]
    public async Task<ActionResult> Profile()
    {
        var user = await userService.GetUserByEmailAsync(User.FindFirst(ClaimTypes.Email)?.Value ?? "");
        if (user is null)
        {
            return NotFound();
        }

        var model = new UserProfileViewModel
        {
            Username = user.UserName,
            Email = user.Email,
            Bio = user.Bio,
        };

        return View(model);
    }
}
