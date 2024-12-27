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
            return RedirectToAction("Index", "Home", new { area = Constants.Areas.Customer });
        }

        var cartItems = await cartItemService.GetCartItemsByUserIdAsync(userId);
        return View(cartItems);
    }

    [HttpPost]
    public async Task<JsonResult> AddToCart(AddToCartDto addToCartDto)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Json(new { success = false, message = "User not authenticated." });
        }

        var success = await cartItemService.AddToCartAsync(addToCartDto, userId);
        if (!success)
        {
            return Json(new { success = false, message = "Failed to add item to cart." });
        }

        return Json(new { success = true });
    }

    [HttpPost]
    public async Task<JsonResult> DeleteCartItem(int id)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Json(new { success = false, message = "User not authenticated." });
        }

        var success = await cartItemService.DeleteCartItemByIdAsync(id, userId);
        if (!success)
        {
            return Json(new { success = false, message = "Failed to delete item from cart." });
        }

        return Json(new { success = true });
    }
}
