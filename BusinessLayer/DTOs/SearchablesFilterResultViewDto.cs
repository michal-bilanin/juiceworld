using BusinessLayer.DTOs;
using Infrastructure.QueryObjects;

namespace PresentationLayer.Mvc.Areas.Customer.Models;

public class SearchablesFilterResultViewDto
{
    public required FilteredResult<ProductDetailDto> Products { get; set; }
    public required FilteredResult<ManufacturerDto> Manufacturers { get; set; }
    public required FilteredResult<TagDto> Tags { get; set; }
}
