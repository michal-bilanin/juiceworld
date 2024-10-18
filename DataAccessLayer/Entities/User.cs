using JuiceWorld.Enums;

namespace JuiceWorld.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int PasswordHashRounds { get; set; }
    public string PasswordSalt { get; set; }
    public UserRole UserRole { get; set; } = UserRole.Customer;
    public string Bio { get; set; }

    public virtual IEnumerable<CartItem> CartItems { get; set; }
    public virtual IEnumerable<WishListItem> WishListItems { get; set; }
    public virtual IEnumerable<Review> Reviews { get; set; }
    public virtual IEnumerable<Order> Orders { get; set; }
    public virtual IEnumerable<Address> Addresses { get; set; }
}