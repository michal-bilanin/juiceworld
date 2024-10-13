using JuiceWorld.Entities;
using System;

public class CartItem: BaseEntity
{
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }

    public int OrderId { get; set; }
    public Product Product { get; set; }
    public User User { get; set; }
    public Order Order { get; set; }
}
