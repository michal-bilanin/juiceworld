using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.QueryObjects;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Areas.Admin.Controllers;

[Area(Constants.Areas.Admin)]
[RedirectIfNotAdminActionFilter]
public class UserController(IUserService userService, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] UserFilterDto userFilterDto)
    {
        var users = await userService.GetUsersFilteredAsync(userFilterDto);
        return View(new FilteredResult<UserSimpleViewModel>
        {
            Entities = mapper.Map<IEnumerable<UserDto>, IEnumerable<UserSimpleViewModel>>(users.Entities),
            PageIndex = users.PageIndex,
            TotalPages = users.TotalPages
        });
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new UserRegisterDto
        {
            UserName = "",
            Email = "",
            Password = "",
            ConfirmPassword = "",
            Bio = ""
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserRegisterDto viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        var createdUser = await userService.RegisterUserAsync(mapper.Map<UserRegisterDto, UserRegisterDto>(viewModel),
            viewModel.UserRole);
        if (createdUser == null)
        {
            ModelState.AddModelError("Email", "A user with this username or email already exists.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user == null)
        {
            ModelState.AddModelError("Id", "User not found.");
            return View();
        }

        return View(mapper.Map<UserDto, UserUpdateDto>(user));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserUpdateDto viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        if (string.IsNullOrEmpty(viewModel.Password))
            viewModel.Password = null; // Do not update password if it is empty

        var updatedUser = await userService.UpdateUserAsync(viewModel);
        if (updatedUser == null)
        {
            ModelState.AddModelError("Email", "A user with this username or email already exists.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await userService.DeleteUserByIdAsync(id);
        if (!deleted) return BadRequest();

        return RedirectToAction(nameof(Index));
    }
}