using BusinessLayer.DTOs;

namespace PresentationLayer.Mvc.Facades.Interfaces;

public interface IOrderCouponFacade
{
    Task<OrderDto?> CreateOrderWithCouponAsync(int userId, CreateOrderDto orderDto);
}