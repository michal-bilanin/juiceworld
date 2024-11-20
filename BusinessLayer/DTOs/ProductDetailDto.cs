using Commons.Enums;

namespace BusinessLayer.DTOs;

public class ProductDetailDto
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Image { get; set; }
    public string Description { get; set; } = null!;

    public ProductCategory Category { get; set; }
    public ProductUsageType UsageType { get; set; }

    public ManufacturerDto Manufacturer { get; set; } = null!;
    public IEnumerable<ReviewDto> Reviews { get; set; } = null!;
}
