using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using PresentationLayer.Mvc.Areas.Customer.Models;

namespace BusinessLayer.Facades;

public class SearchablesFacade(
    IProductService productService,
    IManufacturerService manufacturerService,
    ITagService tagService,
    IMapper mapper) : ISearchablesFacade
{
    public async Task<SearchablesFilterResultViewDto> GetSearchablesFilteredAsync(
        SearchablesFilterViewDto searchablesFilter)
    {
        var productsResult =
            await productService.GetProductDetailsFilteredAsync(
                mapper.Map<SearchablesFilterViewDto, ProductFilterDto>(searchablesFilter));
        var manufacturersResult =
            await manufacturerService.GetManufacturersAsync(
                mapper.Map<SearchablesFilterViewDto, ManufacturerFilterDto>(searchablesFilter));
        var tagsResult =
            await tagService.GetTagsAsync(mapper.Map<SearchablesFilterViewDto, TagFilterDto>(searchablesFilter));

        return new SearchablesFilterResultViewDto
        {
            Products = productsResult,
            Manufacturers = manufacturersResult,
            Tags = tagsResult
        };
    }
}