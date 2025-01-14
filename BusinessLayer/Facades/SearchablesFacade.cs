using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;

namespace BusinessLayer.Facades;

public class SearchablesFacade(
    IProductService productService,
    IManufacturerService manufacturerService,
    ITagService tagService,
    IMapper mapper) : ISearchablesFacade
{
    public async Task<SearchablesFilterResultDto> GetSearchablesFilteredAsync(
        SearchablesFilterDto searchablesFilter)
    {
        var productsResult =
            await productService.GetProductDetailsFilteredAsync(
                mapper.Map<SearchablesFilterDto, ProductFilterDto>(searchablesFilter));
        var manufacturersResult =
            await manufacturerService.GetManufacturersAsync(
                mapper.Map<SearchablesFilterDto, ManufacturerFilterDto>(searchablesFilter));
        var tagsResult =
            await tagService.GetTagsAsync(mapper.Map<SearchablesFilterDto, TagFilterDto>(searchablesFilter));

        return new SearchablesFilterResultDto
        {
            Products = productsResult,
            Manufacturers = manufacturersResult,
            Tags = tagsResult
        };
    }
}