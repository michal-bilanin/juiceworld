using System.ComponentModel.DataAnnotations.Schema;
using Commons.Enums;
using JuiceWorld.Entities.Interfaces;

namespace JuiceWorld.Entities;

public class Order : BaseEntity, IAuditableEntity
{
    public DeliveryType DeliveryType { get; set; } = DeliveryType.Unknown;
    public OrderStatus Status { get; set; } = OrderStatus.Unknown;
    public DateTime? Departure { get; set; }
    public DateTime? Arrival { get; set; }
    public PaymentMethodType PaymentMethodType { get; set; } = PaymentMethodType.Unknown;
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }

    public int AddressId { get; set; }

    [ForeignKey(nameof(AddressId))]
    public virtual Address? Address { get; set; }

    public virtual List<CartItem> CartItems { get; set; } = [];
    public virtual List<OrderProduct> OrderProducts { get; set; } = [];
}
