using JuiceWorld.Constants;

namespace JuiceWorld.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public ProductCategory Category { get; set; } = ProductCategory.Testosterone;
    public ProductUsageType UsageType { get; set; } = ProductUsageType.Injectable;
    public int ManufacturerId { get; set; }
    public virtual Manufacturer Manufacturer { get; set; }
    public virtual IEnumerable<Review> Reviews { get; set; }
    public virtual IEnumerable<CartItem> CartItems { get; set; }
    public virtual IEnumerable<WishListItem> WishListItems { get; set; }
    public virtual IEnumerable<OrderProduct> OrdersProducts { get; set; }
}
