using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;

namespace PresentationLayer.Mvc.Areas.Admin.Controllers;

[Area(Constants.Areas.Admin)]
[RedirectIfNotAdminActionFilter]
public class ManufacturerController(IManufacturerService manufacturerService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] ManufacturerFilterDto manufacturerFilterDto)
    {
        var manufacturers = await manufacturerService.GetManufacturersAsync(manufacturerFilterDto);
        return View(manufacturers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new ManufacturerDto { Name = "" });
    }

    [HttpPost]
    public async Task<IActionResult> Create(ManufacturerDto viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        var createdManufacturer = await manufacturerService.CreateManufacturerAsync(viewModel);
        if (createdManufacturer == null)
        {
            ModelState.AddModelError("Id", "Failed to create manufacturer.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var manufacturer = await manufacturerService.GetManufacturerByIdAsync(id);
        if (manufacturer == null)
        {
            ModelState.AddModelError("Id", "Manufacturer not found.");
            return View(manufacturer);
        }

        return View(manufacturer);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ManufacturerDto viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        var updatedManufacturer = await manufacturerService.UpdateManufacturerAsync(viewModel);
        if (updatedManufacturer == null)
        {
            ModelState.AddModelError("Id", "Failed to update manufacturer.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var isDeleted = await manufacturerService.DeleteManufacturerByIdAsync(id);
        if (!isDeleted)
        {
            ModelState.AddModelError("Id", "Failed to delete manufacturer.");
            return View(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }
}