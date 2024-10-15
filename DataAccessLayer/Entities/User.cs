using JuiceWorld.Entities;
using System;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordHashRounds { get; set; }
    public string PasswordSalt { get; set; }
    public string UserRole { get; set; }
    public string Bio { get; set; }

    public virtual IEnumerable<CartItem> CartItems { get; set; }
    public virtual IEnumerable<WishListItem> WishListItems { get; set; }
    public virtual IEnumerable<Review> Reviews { get; set; }
    public virtual IEnumerable<Order> Orders { get; set; }
    public virtual IEnumerable<Address> Addresses { get; set; }
}
