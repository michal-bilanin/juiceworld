using System.Security.Claims;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using JuiceWorld.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Areas.Customer.Controllers;

[Area(Constants.Areas.Customer)]
public class UserController(IUserService userService,
    IMapper mapper,
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
    public async Task<ActionResult> Register(UserRegisterDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await userService.RegisterUserAsync(new UserRegisterDto
        {
            UserName = model.UserName,
            Email = model.Email,
            Bio = model.Bio,
            Password = model.Password,
            ConfirmPassword = model.ConfirmPassword
        });

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
                new Claim(ClaimTypes.Name, userDb.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, userDb.Email ?? string.Empty),
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
            return View(model);
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            var ci = new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
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
    [RedirectIfNotAuthenticatedActionFilter]
    public async Task<ActionResult> Profile()
    {
        var user = await userService.GetUserByEmailAsync(User.FindFirst(ClaimTypes.Email)?.Value ?? "");
        if (user is null)
        {
            return NotFound();
        }

        return View(mapper.Map<UserDto, UserProfileViewModel>(user));
    }

    [HttpGet]
    [RedirectIfNotAuthenticatedActionFilter]
    public async Task<IActionResult> Edit()
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.Sid)?.Value, out var userId))
        {
            return BadRequest();
        }

        var user = await userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return BadRequest();
        }

        return View(mapper.Map<UserDto, UserUpdateRestrictedViewModel>(user));
    }

    [HttpPost]
    [RedirectIfNotAuthenticatedActionFilter]
    public async Task<IActionResult> Edit(UserUpdateRestrictedViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        if (!int.TryParse(User.FindFirst(ClaimTypes.Sid)?.Value, out var userId))
        {
            return BadRequest();
        }

        if (viewModel.Id != userId)
        {
            return BadRequest();
        }

        if (string.IsNullOrEmpty(viewModel.Password))
        {
            viewModel.Password = null; // Do not update password if it is empty
        }

        var updatedUser = await userService.UpdateUserAsync(mapper.Map<UserUpdateRestrictedViewModel, UserUpdateDto>(viewModel));
        if (updatedUser == null)
        {
            ModelState.AddModelError("Email", "A user with this username or email already exists.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Profile));
    }
}
