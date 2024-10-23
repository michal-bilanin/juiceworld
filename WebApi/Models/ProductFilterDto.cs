using JuiceWorld.Enums;

namespace WebApi.Models;

public class ProductFilterDto
{
    public string? MmanufacturerName { get; set; }
    public string? Category { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? PriceMax { get; set; }
    public decimal? PriceMin { get; set; }
}