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

    private IQueryObject<Product> GetQueryObject(ProductFilterDto productFilter)
    {
        Enum.TryParse<ProductCategory>(productFilter.Category, true, out var categoryEnum);

        return queryObject.Filter(p =>
                (productFilter.Category == null || p.Category == categoryEnum) &&
                (productFilter.PriceMax == null || p.Price <= productFilter.PriceMax) &&
                (productFilter.PriceMin == null || p.Price >= productFilter.PriceMin) &&
                (productFilter.ManufacturerId == null || p.ManufacturerId == productFilter.ManufacturerId) &&
                (productFilter.TagId == null || p.Tags.Any(t => t.Id == productFilter.TagId)) &&
                (p.Manufacturer == null || productFilter.NameQuery == null ||
                 p.Name.ToLower().Contains(productFilter.NameQuery.ToLower()) ||
                 p.Description.ToLower().Contains(productFilter.NameQuery.ToLower())))
            .OrderBy(p => p.Id)
            .Paginate(productFilter.PageIndex, productFilter.PageSize);
    }

    public async Task<FilteredResult<ProductDto>> GetProductsFilteredAsync(ProductFilterDto productFilter)
    {
        var query = GetQueryObject(productFilter);
        query.Include(nameof(Product.Tags), nameof(Product.Manufacturer), nameof(Product.Reviews));
        var filteredProducts = await query.ExecuteAsync();

        return new FilteredResult<ProductDto>
        {
            Entities = mapper.Map<List<ProductDto>>(filteredProducts.Entities),
            PageIndex = filteredProducts.PageIndex,
            TotalPages = filteredProducts.TotalPages
        };
    }

    public async Task<FilteredResult<ProductDetailDto>> GetProductDetailsFilteredAsync(ProductFilterDto productFilter)
    {
        var query = GetQueryObject(productFilter);
        var filteredProducts = await query.ExecuteAsync();
        return new FilteredResult<ProductDetailDto>
        {
            Entities = mapper.Map<List<ProductDetailDto>>(filteredProducts.Entities),
            PageIndex = filteredProducts.PageIndex,
            TotalPages = filteredProducts.TotalPages
        };
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id, nameof(Product.Tags));
        return product is null ? null : mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDetailDto?> GetProductDetailByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id, nameof(Product.Manufacturer),
            nameof(Product.Reviews), $"{nameof(Product.Reviews)}.{nameof(Review.User)}", nameof(Product.Tags));

        if (product is null)
        {
            return null;
        }

        product.Reviews = product.Reviews.OrderByDescending(r => r.CreatedAt).ToList();
        return mapper.Map<ProductDetailDto>(product);
    }

    public async Task<ProductDto?> UpdateProductAsync(ProductDto productDto)
    {
        var oldProduct = await productRepository.GetByIdAsync(productDto.Id);
        if (oldProduct is null)
        {
            return null;
        }

        var updatedProduct = await productRepository.UpdateAsync(mapper.Map<Product>(productDto));
        return updatedProduct is null ? null : mapper.Map<ProductDto>(updatedProduct);
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        return await productRepository.DeleteAsync(id);
    }
}
