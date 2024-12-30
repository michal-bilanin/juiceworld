using System.Security.Claims;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;

namespace PresentationLayer.Mvc.Areas.Customer.Controllers;

[Area(Constants.Areas.Customer)]
[RedirectIfNotAuthenticatedActionFilter]
public class OrderController(IOrderService orderService, ICartItemService cartItemService) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index([FromQuery] PaginationDto pagination)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Unauthorized();
        }

        var orders = await orderService.GetOrdersByUserIdAsync(userId, pagination);
        return View(orders);
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Unauthorized();
        }

        var order = await orderService.GetOrderDetailByIdAsync(id);
        if (order is null || order.UserId != userId)
        {
            return Unauthorized();
        }

        return View(order);
    }

    [HttpGet]
    public async Task<ActionResult> Create()
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Unauthorized();
        }

        var cartItems = await cartItemService.GetCartItemsByUserIdAsync(userId);
        return View(new CreateOrderDto { UserId = userId, CartItems = cartItems });
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateOrderDto orderDto)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return View();
        }

        if (orderDto.UserId != userId)
        {
            return BadRequest();
        }

        var order = await orderService.ExecuteOrderAsync(orderDto);
        if (order is null)
        {
            ModelState.AddModelError("Order", "Failed to create order.");
            return View();
        }

        return RedirectToAction(nameof(Details), new { order.Id });
    }
}
