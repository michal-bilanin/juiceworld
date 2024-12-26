using Commons.Enums;

namespace BusinessLayer.DTOs;

public class OrderDetailDto
{
    public int Id { get; set; }
    public DeliveryType DeliveryType { get; set; } = DeliveryType.Standard;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime? Departure { get; set; }
    public DateTime? Arrival { get; set; }
    public PaymentMethodType PaymentMethodType { get; set; } = PaymentMethodType.Monero;
    public UserDto? User { get; set; }
    public int UserId { get; set; }

    public AddressDto? Address { get; set; }
    public int AddressId { get; set; }
    public List<OrderProductDetailDto> OrderProducts { get; set; } = [];
}
