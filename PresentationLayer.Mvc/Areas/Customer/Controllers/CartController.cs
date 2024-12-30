using System.Security.Claims;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;

namespace PresentationLayer.Mvc.Areas.Customer.Controllers;

[Area(Constants.Areas.Customer)]
[RedirectIfNotAuthenticatedActionFilter]
public class CartController(ICartItemService cartItemService) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Unauthorized();
        }

        var cartItems = await cartItemService.GetCartItemsByUserIdAsync(userId);
        return View(cartItems);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(AddToCartDto addToCartDto)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Unauthorized();
        }

        var success = await cartItemService.AddToCartAsync(addToCartDto, userId);
        if (!success)
        {
            ViewData[Constants.Keys.ErrorMessage] = "Failed to add item to cart.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCartItem(int id)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Unauthorized();
        }

        var success = await cartItemService.DeleteCartItemByIdAsync(id, userId);
        if (!success)
        {
            ViewData[Constants.Keys.ErrorMessage] = "Failed to delete item from cart.";
        }

        return RedirectToAction(nameof(Index));
    }
}
