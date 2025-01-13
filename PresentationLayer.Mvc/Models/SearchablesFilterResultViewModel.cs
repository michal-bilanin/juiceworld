using BusinessLayer.DTOs;
using Infrastructure.QueryObjects;

namespace PresentationLayer.Mvc.Models;

public class SearchablesFilterResultViewModel
{
    public required FilteredResult<ProductDetailDto> Products { get; set; }
    public required FilteredResult<ManufacturerDto> Manufacturers { get; set; }
    public required FilteredResult<TagDto> Tags { get; set; }
}