using System.ComponentModel.DataAnnotations.Schema;
using JuiceWorld.Enums;

namespace JuiceWorld.Entities;

public class Order : BaseEntity
{
    public DeliveryType DeliveryType { get; set; } = DeliveryType.Standard;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime? Departure { get; set; }
    public DateTime? Arrival { get; set; }
    public PaymentMethodType PaymentMethodType { get; set; } = PaymentMethodType.Monero;
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    public int AddressId { get; set; }

    [ForeignKey(nameof(AddressId))]
    public virtual Address Address { get; set; }

    public virtual IEnumerable<CartItem> CartItems { get; set; }
    public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }
}
