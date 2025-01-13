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
            if (!await imageService.SaveImageAsync(productImageDto.ImageValue, imageName))
            {
                return null;
            }
            productImageDto.Image = imageName;
        }
        return await productService.CreateProductAsync(mapper.Map<ProductDto>(productImageDto));
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return await productService.GetAllProductsAsync();
    }

    public async Task<FilteredResult<ProductDto>> GetProductsFilteredAsync(ProductFilterDto productFilter)
    {
        return await productService.GetProductsFilteredAsync(productFilter);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        return await productService.GetProductByIdAsync(id);
    }

    public async Task<ProductDetailDto?> GetProductDetailByIdAsync(int id)
    {
        var prod = await productService.GetProductDetailByIdAsync(id);
        var ret = mapper.Map<ProductDetailDto>(prod);
        ret.ImageValue = await imageService.GetImageAsync(ret.Image ?? string.Empty);
        return ret;
    }

    public async Task<ProductDto?> UpdateProductAsync(ProductImageDto productDto)
    {
        var product = await productService.GetProductByIdAsync(productDto.Id);
        if (product == null)
            return null;

        if (!string.IsNullOrEmpty(productDto.ImageValue))
        {
            var imageName = GeneratedImageName(productDto.ImageValue);
            if (!await imageService.UpdateImageAsync(productDto.ImageValue, product.Image, imageName))
            {
                return null;
            }
            productDto.Image = imageName;
        }
        return await productService.UpdateProductAsync(mapper.Map<ProductDto>(productDto));
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product is null)
        {
            return false;
        }
        
        return (product.Image == null || imageService.DeleteImage(product.Image)) && 
               await productService.DeleteProductByIdAsync(product.Id);
    }
}