using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services.Interfaces;

public interface IProductService
{
    Task<ProductDto?> CreateProductAsync(ProductDto productDto);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<IEnumerable<ProductDto>> GetProductsFilteredAsync(ProductFilterDto productFilter);
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDetailDto?> GetProductDetailByIdAsync(int id);
    Task<ProductDto?> UpdateProductAsync(ProductDto productDto);
    Task<bool> DeleteProductByIdAsync(int id);
}
