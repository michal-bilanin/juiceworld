using JuiceWorld.Entities;
using System;

public class Order : BaseEntity
{
    public string DeliveryType { get; set; }
    public string Status { get; set; }
    public string Departure { get; set; }
    public string Arrival { get; set; }
    public string PaymentMethodType { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int AddressId { get; set; }
    public virtual Address Address { get; set; }
    public virtual IEnumerable<CartItem> CartItems { get; set; }
    public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }
}
