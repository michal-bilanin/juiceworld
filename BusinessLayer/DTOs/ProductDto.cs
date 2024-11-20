using Commons.Enums;

namespace BusinessLayer.DTOs;

public class ProductDto : BaseEntityDto
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Image { get; set; }
    // public string? ImageName { get; set; }
    public string Description { get; set; } = null!;
    public ProductCategory Category { get; set; } = ProductCategory.Testosterone;
    public ProductUsageType UsageType { get; set; } = ProductUsageType.Injectable;
    public int ManufacturerId { get; set; }
}
