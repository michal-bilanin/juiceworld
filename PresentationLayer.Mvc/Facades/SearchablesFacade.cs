using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using PresentationLayer.Mvc.Areas.Customer.Models;
using PresentationLayer.Mvc.Facades.Interfaces;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Facades;

public class SearchablesFacade(
    IProductService productService,
    IManufacturerService manufacturerService,
    ITagService tagService,
    IMapper mapper) : ISearchablesFacade
{
    public async Task<SearchablesFilterResultViewModel> GetSearchablesFilteredAsync(
        SearchablesFilterViewModel searchablesFilter)
    {
        var productsResult =
            await productService.GetProductDetailsFilteredAsync(
                mapper.Map<SearchablesFilterViewModel, ProductFilterDto>(searchablesFilter));
        var manufacturersResult =
            await manufacturerService.GetManufacturersAsync(
                mapper.Map<SearchablesFilterViewModel, ManufacturerFilterDto>(searchablesFilter));
        var tagsResult =
            await tagService.GetTagsAsync(mapper.Map<SearchablesFilterViewModel, TagFilterDto>(searchablesFilter));

        return new SearchablesFilterResultViewModel
        {
            Products = productsResult,
            Manufacturers = manufacturersResult,
            Tags = tagsResult
        };
    }
}