using JuiceWorld.Enums;

namespace WebApi.Models;

public class ProductFilterDto
{
    public string? MmanufacturerName { get; set; }
    public ProductCategory? Category { get; set; }
}