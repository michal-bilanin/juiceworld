using BusinessLayer.DTOs;

namespace PresentationLayer.Mvc.Models;

public class ProductEditViewModel
{
    public required ProductImageDto Product { get; set; }
    public IEnumerable<ManufacturerDto> AllManufacturers { get; set; } = [];
    public IEnumerable<TagDto> AllTags { get; set; } = [];

    public IFormFile? ImageFile { get; set; }
}

