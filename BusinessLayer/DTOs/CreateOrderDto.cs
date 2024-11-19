using Commons.Enums;

namespace BusinessLayer.DTOs;

public class CreateOrderDto : BaseEntityDto
{
    public DeliveryType DeliveryType { get; set; } = DeliveryType.Standard;
    public PaymentMethodType PaymentMethodType { get; set; } = PaymentMethodType.Monero;
    public int UserId { get; set; }
    public int AddressId { get; set; }
}
