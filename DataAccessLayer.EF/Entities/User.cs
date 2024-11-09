using Commons.Enums;

namespace JuiceWorld.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int PasswordHashRounds { get; set; }
    public string PasswordSalt { get; set; } = null!;
    public UserRole UserRole { get; set; } = UserRole.Customer;
    public string Bio { get; set; } = null!;

    public virtual IEnumerable<CartItem> CartItems { get; set; } = null!;
    public virtual IEnumerable<WishListItem> WishListItems { get; set; } = null!;
    public virtual IEnumerable<Review> Reviews { get; set; } = null!;
    public virtual IEnumerable<Order> Orders { get; set; } = null!;
    public virtual IEnumerable<Address> Addresses { get; set; } = null!;
}
