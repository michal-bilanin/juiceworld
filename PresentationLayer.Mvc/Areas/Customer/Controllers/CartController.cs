using System.Security.Claims;
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
}
