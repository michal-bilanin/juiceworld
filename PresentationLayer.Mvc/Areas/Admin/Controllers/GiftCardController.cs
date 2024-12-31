using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;

namespace PresentationLayer.Mvc.Areas.Admin.Controllers;

[Area(Constants.Areas.Admin)]
[RedirectIfNotAdminActionFilter]
public class GiftCardController(IGiftCardService giftCardService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GiftCardFilterDto manufacturerFilterDto)
    {
        var allGiftCards = await giftCardService.GetAllGiftCardsAsync(manufacturerFilterDto);
        return View(allGiftCards);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new GiftCardCreateDto
        {
            Name = "",
            Discount = 0,
            CouponsCount = 0,
            StartDateTime = null,
            ExpiryDateTime = null
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(GiftCardCreateDto viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var createdManufacturer = await giftCardService.CreateGiftCardAsync(viewModel);
        if (createdManufacturer == null)
        {
            ModelState.AddModelError("Id", "Failed to create gift-card.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var giftCard = await giftCardService.GetGiftCardByIdAsync(id);
        if (giftCard == null)
        {
            ModelState.AddModelError("Id", "Manufacturer not found.");
            return View(new GiftCardEditDto());
        }

        return View(new GiftCardEditDto { Name = giftCard.Name });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GiftCardEditDto viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var updatedManufacturer = await giftCardService.UpdateGiftCardAsync(viewModel);
        if (updatedManufacturer == null)
        {
            ModelState.AddModelError("Id", "Failed to update manufacturer.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var giftCard = await giftCardService.GetGiftCardByIdAsync(id);
        if (giftCard == null)
        {
            ModelState.AddModelError("Id", "Gift-card not found.");
            return RedirectToAction(nameof(Index));
        }

        return View(giftCard);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var isDeleted = await giftCardService.DeleteGiftCardByIdAsync(id);
        if (!isDeleted)
        {
            ModelState.AddModelError("Id", "Failed to delete manufacturer.");
            return View(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }
}
