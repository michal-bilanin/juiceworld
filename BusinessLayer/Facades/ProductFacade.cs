using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using Infrastructure.QueryObjects;

namespace BusinessLayer.Facades;

public class ProductFacade(IProductService productService, IImageService imageService, IMapper mapper) : IProductFacade
{
    private string GeneratedImageName(string image)
    {
        return $"{Guid.NewGuid()}{imageService.GetImageExtension(image)}";
    }

    public async Task<ProductDto?> CreateProductAsync(ProductImageDto productImageDto)
    {
        if (!string.IsNullOrEmpty(productImageDto.ImageValue))
        {
            var imageName = GeneratedImageName(productImageDto.ImageValue);
            var imageUrl = await imageService.SaveImageAsync(productImageDto.ImageValue, imageName);
            if (imageUrl == null)
            {
                return null;
            }

            productImageDto.ImageName = imageName;
            productImageDto.ImageUrl = imageUrl;
        }
        return await productService.CreateProductAsync(mapper.Map<ProductDto>(productImageDto));
    }

    public Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return productService.GetAllProductsAsync();
    }

    public Task<FilteredResult<ProductDto>> GetProductsFilteredAsync(ProductFilterDto productFilter)
    {
        return productService.GetProductsFilteredAsync(productFilter);
    }

    public Task<ProductDto?> GetProductByIdAsync(int id)
    {
        return productService.GetProductByIdAsync(id);
    }

    public async Task<ProductDetailDto?> GetProductDetailByIdAsync(int id)
    {
        var prod = await productService.GetProductDetailByIdAsync(id);
        var ret = mapper.Map<ProductDetailDto>(prod);
        return ret;
    }

    public async Task<ProductDto?> UpdateProductAsync(ProductImageDto productDto, int userId)
    {
        var product = await productService.GetProductByIdAsync(productDto.Id);
        if (product == null)
            return null;

        if (!string.IsNullOrEmpty(productDto.ImageValue))
        {
            var imageName = GeneratedImageName(productDto.ImageValue);
            var imageUrl = await imageService.UpdateImageAsync(productDto.ImageValue, product.ImageName, imageName);
            if (imageUrl == null)
            {
                return null;
            }

            productDto.ImageName = imageName;
            productDto.ImageUrl = imageUrl;
        }
        return await productService.UpdateProductAsync(mapper.Map<ProductDto>(productDto), userId);
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product is null)
        {
            return false;
        }

        return (product.ImageName == null || await imageService.DeleteImageAsync(product.ImageName)) &&
               await productService.DeleteProductByIdAsync(product.Id);
    }
}
