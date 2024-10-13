using JuiceWorld.Entities;
using System;

public class WishListItem: BaseEntity
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public Product Product { get; set; }
    public User User { get; set; }
}
