namespace BusinessLayer.DTOs;

public class ProductFilterDto
{
    public string? ManufacturerName { get; set; }
    public string? Category { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? PriceMax { get; set; }
    public decimal? PriceMin { get; set; }
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}
