using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using Infrastructure.QueryObjects;

namespace BusinessLayer.Facades;

public class ProductFacade(IProductService _productService, IImageService _imageService, IMapper _mapper) : IProductFacade
{
    private string GeneratedImageName(ProductImageDto productImageDto)
    {
        return $"{Guid.NewGuid()}{_imageService.GetImageExtension(productImageDto.ImageValue)}";
    }

    public async Task<ProductDto?> CreateProductAsync(ProductImageDto productImageDto)
    {
        if (!string.IsNullOrEmpty(productImageDto.Image))
        {
            var imageName = GeneratedImageName(productImageDto);
            if (!await _imageService.SaveImageAsync(productImageDto.ImageValue, imageName))
            {
                return null;
            }
            productImageDto.Image = imageName;
        }
        return await _productService.CreateProductAsync(_mapper.Map<ProductDto>(productImageDto));
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return await _productService.GetAllProductsAsync();
    }

    public async Task<FilteredResult<ProductDto>> GetProductsFilteredAsync(ProductFilterDto productFilter)
    {
        return await _productService.GetProductsFilteredAsync(productFilter);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        return await _productService.GetProductByIdAsync(id);
    }

    public async Task<ProductDetailDto?> GetProductDetailByIdAsync(int id)
    {
        var prod = await _productService.GetProductDetailByIdAsync(id);
        var ret = _mapper.Map<ProductDetailDto>(prod);
        ret.ImageValue = await _imageService.GetImageAsync(ret.Image);
        return ret;
    }

    public async Task<ProductDto?> UpdateProductAsync(ProductImageDto productDto)
    {
        if (!string.IsNullOrEmpty(productDto.Image))
        {
            var imageName = GeneratedImageName(productDto);
            if (!await _imageService.UpdateImageAsync(productDto.ImageValue, productDto.Image, imageName))
            {
                return null;
            }
            productDto.Image = imageName;
        }
        return await _productService.UpdateProductAsync(_mapper.Map<ProductDto>(productDto));
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product is null)
        {
            return false;
        }
        return product.Image == null || _imageService.DeleteImageAsync(product.Image);
    }
}