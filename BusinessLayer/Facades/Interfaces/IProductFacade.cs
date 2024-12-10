using BusinessLayer.DTOs;

namespace BusinessLayer.Facades.Interfaces;

public interface IProductFacade
{
    Task<ProductDto?> CreateProductAsync(ProductImageDto productDto);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<IEnumerable<ProductDto>> GetProductsFilteredAsync(ProductFilterDto productFilter);
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<ProductDetailDto?> GetProductDetailByIdAsync(int id);
    Task<ProductDto?> UpdateProductAsync(ProductImageDto productDto);
    Task<bool> DeleteProductByIdAsync(int id);
}