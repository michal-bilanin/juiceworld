using System.ComponentModel.DataAnnotations;
using Commons.Enums;
using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.Entities;

[Index(nameof(Email), IsUnique = true)]
public class User : BaseEntity
{
    [MaxLength(100)]
    public string UserName { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
    public int PasswordHashRounds { get; set; }
    public string PasswordSalt { get; set; } = null!;
    public UserRole UserRole { get; set; } = UserRole.Customer;

    [MaxLength(300)]
    public string Bio { get; set; } = null!;

    public virtual IEnumerable<CartItem> CartItems { get; set; } = null!;
    public virtual IEnumerable<WishListItem> WishListItems { get; set; } = null!;
    public virtual IEnumerable<Review> Reviews { get; set; } = null!;
    public virtual IEnumerable<Order> Orders { get; set; } = null!;
    public virtual IEnumerable<Address> Addresses { get; set; } = null!;
}
