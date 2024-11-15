using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Commons.Enums;
using JuiceWorld.Entities.Interfaces;

namespace JuiceWorld.Entities;

public class Product : BaseEntity, IAuditableEntity
{
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Range(0, int.MaxValue)]
    public decimal Price { get; set; }

    [Url]
    public string? ImageUrl { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; } = null!;

    public ProductCategory Category { get; set; } = ProductCategory.Testosterone;
    public ProductUsageType UsageType { get; set; } = ProductUsageType.Injectable;
    public int ManufacturerId { get; set; }

    [ForeignKey(nameof(ManufacturerId))]
    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual IEnumerable<Review> Reviews { get; set; } = null!;
    public virtual IEnumerable<CartItem> CartItems { get; set; } = null!;
    public virtual IEnumerable<WishListItem> WishListItems { get; set; } = null!;
    public virtual IEnumerable<OrderProduct> OrdersProducts { get; set; } = null!;
}
