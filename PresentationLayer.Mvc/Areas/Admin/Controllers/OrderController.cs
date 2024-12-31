using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Mvc.ActionFilters;

namespace PresentationLayer.Mvc.Areas.Admin.Controllers;

[Area(Constants.Areas.Admin)]
[RedirectIfNotAdminActionFilter]
public class OrderController(IOrderService orderService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] PaginationDto pagination)
    {
        var orders = await orderService.GetOrdersAsync(pagination);
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var order = await orderService.GetOrderDetailByIdAsync(id);
        if (order == null)
        {
            ModelState.AddModelError("Id", "Order not found.");
            return View(order);
        }

        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(OrderDetailDto viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var updatedOrder = await orderService.UpdateOrderAsync(viewModel);
        if (updatedOrder == null)
        {
            ModelState.AddModelError(nameof(OrderDetailDto.Status), "Failed to update order.");
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await orderService.DeleteOrderByIdAsync(id))
        {
            ModelState.AddModelError("Id", "Failed to delete order.");
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }
}
