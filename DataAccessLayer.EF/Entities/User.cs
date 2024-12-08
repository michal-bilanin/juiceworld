using System.ComponentModel.DataAnnotations;
using Commons.Enums;
using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.Entities;

[Index(nameof(Email), IsUnique = true)]
public class User : BaseEntity
{
    [MaxLength(100)]
    public required string UserName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    public required string PasswordHash { get; set; }
    public int PasswordHashRounds { get; set; }
    public required string PasswordSalt { get; set; }
    public UserRole UserRole { get; set; } = UserRole.Unknown;

    [MaxLength(300)]
    public required string Bio { get; set; }

    public virtual List<CartItem> CartItems { get; set; } = [];
    public virtual List<WishListItem> WishListItems { get; set; } = [];
    public virtual List<Review> Reviews { get; set; } = [];
    public virtual List<Order> Orders { get; set; } = [];
    public virtual List<Address> Addresses { get; set; } = [];
    public virtual List<AuditTrail> AuditTrails { get; set; } = [];
}
