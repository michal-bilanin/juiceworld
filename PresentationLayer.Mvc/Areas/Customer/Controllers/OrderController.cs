using System.Security.Claims;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;
using PresentationLayer.Mvc.Facades.Interfaces;

namespace PresentationLayer.Mvc.Areas.Customer.Controllers;

[Area(Constants.Areas.Customer)]
[RedirectIfNotAuthenticatedActionFilter]
public class OrderController(
    IOrderService orderService,
    ICartItemService cartItemService,
    IOrderCouponFacade orderCouponFacade) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index([FromQuery] PaginationDto pagination)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return BadRequest();
        }

        var orders = await orderService.GetOrdersByUserIdAsync(userId, pagination);
        return View(orders);
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return BadRequest();
        }

        var order = await orderService.GetOrderDetailByIdAsync(id);
        if (order is null || order.UserId != userId) return Unauthorized();

        return View(order);
    }

    [HttpGet]
    public async Task<ActionResult> Create()
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return BadRequest();
        }

        var cartItems = await cartItemService.GetCartItemsByUserIdAsync(userId);

        return View(new CreateOrderDto { UserId = userId, CartItems = cartItems });
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateOrderDto orderDto)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty, out var userId))
        {
            return BadRequest();
        }
        var cartItems = await cartItemService.GetCartItemsByUserIdAsync(userId);


        if (!ModelState.IsValid)
        {
            orderDto.CartItems = cartItems;
            return View(orderDto);
        }

        if (orderDto.UserId != userId) return BadRequest();

        var order = await orderCouponFacade.CreateOrderWithCouponAsync(userId, orderDto);
        if (order is null)
        {
            ModelState.AddModelError("Order", "Failed to create order.");
            orderDto.CartItems = cartItems;
            return View(orderDto);
        }

        return RedirectToAction(nameof(Details), new { order.Id });
    }
}
