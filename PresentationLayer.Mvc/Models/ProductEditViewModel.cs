using BusinessLayer.DTOs;

namespace PresentationLayer.Mvc.Models;

public class ProductEditViewModel
{
    public required ProductDto Product { get; set; }
    public IEnumerable<ManufacturerDto> AllManufacturers { get; set; } = [];
    public IEnumerable<TagDto> AllTags { get; set; } = [];
}