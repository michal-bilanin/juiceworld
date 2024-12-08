using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Commons.Enums;
using JuiceWorld.Entities.Interfaces;

namespace JuiceWorld.Entities;

public class Product : BaseEntity, IAuditableEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }

    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }

    public string? Image { get; set; }

    [MaxLength(1000)]
    public required string Description { get; set; }

    public ProductCategory Category { get; set; } = ProductCategory.Testosterone;
    public ProductUsageType UsageType { get; set; } = ProductUsageType.Injectable;
    public int ManufacturerId { get; set; }

    [ForeignKey(nameof(ManufacturerId))]
    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual IEnumerable<Review> Reviews { get; set; } = [];
    public virtual IEnumerable<CartItem> CartItems { get; set; } = [];
    public virtual IEnumerable<WishListItem> WishListItems { get; set; } = [];
    public virtual IEnumerable<OrderProduct> OrdersProducts { get; set; } = [];
}
