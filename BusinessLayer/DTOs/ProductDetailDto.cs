using Commons.Enums;

namespace BusinessLayer.DTOs;

public class ProductDetailDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
    public string Description { get; set; }

    public ProductCategory Category { get; set; }
    public ProductUsageType UsageType { get; set; }

    public ManufacturerDto Manufacturer { get; set; }
    public IEnumerable<ReviewDto> Reviews { get; set; }
}
