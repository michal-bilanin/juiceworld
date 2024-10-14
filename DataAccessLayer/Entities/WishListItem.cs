using JuiceWorld.Entities;
using System;

public class WishListItem: BaseEntity
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public virtual Product Product { get; set; }
    public virtual User User { get; set; }
}
