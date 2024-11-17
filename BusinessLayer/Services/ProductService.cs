using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using JuiceWorld.Entities;

namespace BusinessLayer.Services;

public class ProductService(
    IRepository<Product> productRepository,
    IMapper mapper,
    IQueryObject<Product> queryObject) : IProductService
{
    public async Task<ProductDto?> CreateProductAsync(ProductDto productDto)
    {
        var newProduct = await productRepository.CreateAsync(mapper.Map<Product>(productDto));
        return newProduct is null ? null : mapper.Map<ProductDto>(newProduct);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await productRepository.GetAllAsync();
        return mapper.Map<List<ProductDto>>(products);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsFilteredAsync(ProductFilterDto productFilter)
    {
        Enum.TryParse<ProductCategory>(productFilter.Category, true, out var categoryEnum);
        var query = queryObject.Filter(p =>
            (productFilter.ManufacturerName == null ||
             p.Manufacturer.Name.ToLower().Contains(productFilter.ManufacturerName.ToLower())) &&
            (productFilter.Category == null || p.Category == categoryEnum) &&
            (productFilter.PriceMax == null || p.Price <= productFilter.PriceMax) &&
            (productFilter.PriceMin == null || p.Price >= productFilter.PriceMin) &&
            (productFilter.Name == null || p.Name.ToLower().Contains(productFilter.Name.ToLower())) &&
            (productFilter.Description == null ||
             p.Description.ToLower().Contains(productFilter.Description.ToLower())));

        if (productFilter is { PageIndex: not null, PageSize: not null })
        {
            query = query.Paginate(productFilter.PageIndex.Value, productFilter.PageSize.Value);
        }

        var result = await query.ExecuteAsync();
        return mapper.Map<ICollection<ProductDto>>(result);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        return product is null ? null : mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDetailDto?> GetProductDetailByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id, nameof(Product.Manufacturer),
            nameof(Product.Reviews));
        return product is null ? null : mapper.Map<ProductDetailDto>(product);
    }

    public async Task<ProductDto?> UpdateProductAsync(ProductDto productDto)
    {
        var updatedProduct = await productRepository.UpdateAsync(mapper.Map<Product>(productDto));
        return updatedProduct is null ? null : mapper.Map<ProductDto>(updatedProduct);
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        return await productRepository.DeleteAsync(id);
    }
}
