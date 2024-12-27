using System.Security.Claims;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using JuiceWorld.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Controllers;

public class UserController(IUserService userService,
    IMapper mapper, 
    UserManager<User> userManager,
    SignInManager<User> signInManager) : Controller
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
            UserName = model.Username,
            Email = model.Email,
            Bio = model.Bio,
            UserRole = UserRole.Customer
        };
        
        var result = await userManager.CreateAsync(user, model.Password);
        
        var userDb = await signInManager.UserManager.FindByEmailAsync(model.Email);
        if (userDb is null)
        {
            ModelState.AddModelError(string.Empty, "User doesn't exist.");
            return Unauthorized();
        }
        
        if (result.Succeeded)
        {
            var ci = new[]
            {
                new Claim(ClaimTypes.Sid, userDb.Id.ToString()),
                new Claim(ClaimTypes.Name, userDb.UserName),
                new Claim(ClaimTypes.Email, userDb.Email),
                new Claim(ClaimTypes.Role, userDb.UserRole.ToString())
            };

            await signInManager.SignInWithClaimsAsync(userDb, false, ci);
            return RedirectToAction(nameof(Index), "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

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
        var user = await signInManager.UserManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User doesn't exist.");
            return Unauthorized();
        }
        
        var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            var ci = new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };

            await signInManager.SignInWithClaimsAsync(user, false, ci);
            return RedirectToAction(nameof(Index), "Home");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

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
