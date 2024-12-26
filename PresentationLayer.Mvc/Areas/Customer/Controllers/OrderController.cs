using System.Security.Claims;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Mvc.Areas.Customer.Controllers;

[Area(Constants.Areas.Customer)]
public class OrderController(IOrderService orderService) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index([FromQuery] PaginationDto pagination)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return RedirectToAction("Index", "Home", new { area = Constants.Areas.Customer });
        }

        var orders = await orderService.GetOrdersByUserIdAsync(userId, pagination);
        return View(orders);
    }
}
