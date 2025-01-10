using System.Security.Claims;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;

namespace PresentationLayer.Mvc.Areas.Customer.Controllers;

[Area(Constants.Areas.Customer)]
[RedirectIfNotAuthenticatedActionFilter]
public class WishlistController(IWishListItemService wishListItemService, ICartItemService cartItemService) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId);

        var wishListItems = await wishListItemService.GetWishListItemsByUserIdAsync(userId);
        return View(wishListItems);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var success = await wishListItemService.DeleteWishListItemByIdAsync(id);
        if (!success)
        {
            ViewData[Constants.Keys.ErrorMessage] = "Failed to delete item from wishlist.";
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> AddToCart(int productId)
    {
        int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId);

        var addToCartDto = new AddToCartDto
        {
            ProductId = productId,
            Quantity = 1
        };

        var success = await cartItemService.AddToCartAsync(addToCartDto, userId);
        if (!success)
        {
            ViewData[Constants.Keys.ErrorMessage] = "Failed to add item to cart.";
        }

        return RedirectToAction(nameof(Index));
    }
}
