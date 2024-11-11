using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services.Interfaces;

public interface IOrderService
{
    Task<OrderDto?> CreateOrderAsync(OrderDto orderDto);
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto?> GetOrderByIdAsync(int id);
    Task<OrderDto?> UpdateOrderAsync(OrderDto orderDto);
    Task<bool> DeleteOrderByIdAsync(int id);
}
