using System.ComponentModel.DataAnnotations.Schema;
using Commons.Enums;

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
    public virtual User User { get; set; } = null!;

    public int AddressId { get; set; }

    [ForeignKey(nameof(AddressId))]
    public virtual Address Address { get; set; } = null!;

    public virtual IEnumerable<CartItem> CartItems { get; set; } = null!;
    public virtual IEnumerable<OrderProduct> OrderProducts { get; set; } = null!;
}
