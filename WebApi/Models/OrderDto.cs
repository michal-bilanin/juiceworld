using JuiceWorld.Enums;

namespace JuiceWorld.Entities;

public class OrderDto : BaseEntityDto
{
    public DeliveryType DeliveryType { get; set; } = DeliveryType.Standard;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime? Departure { get; set; }
    public DateTime? Arrival { get; set; }
    public PaymentMethodType PaymentMethodType { get; set; } = PaymentMethodType.Monero;
    public int UserId { get; set; }
    public int AddressId { get; set; }
}
