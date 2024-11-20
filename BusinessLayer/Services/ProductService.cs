using System.Buffers.Text;
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
    private const string ImgFolderPath = "Images";
    private static readonly Dictionary<string, string> MimeTypes = new()
    {
        // enough to determine the image type
        { "/9j/", ".jpg" },
        { "iVBORw0KGgo", ".png" },
        { "R0lGODlh", ".gif" },
        { "R0lGODdh", ".gif" }
    };

    private void SaveImage(string base64Image, string imageName)
    {
        Directory.CreateDirectory(ImgFolderPath);
        var imageBytes = Convert.FromBase64String(base64Image);
        var filePath = Path.Combine(ImgFolderPath, imageName);
        File.WriteAllBytes(filePath, imageBytes);
    }

    public static string GetImageExtension(string base64Image)
    {
        foreach (var mimeType in MimeTypes)
        {
            if (base64Image.StartsWith(mimeType.Key))
            {
                return mimeType.Value;
            }
        }
        return string.Empty;
    }

    public async Task<ProductDto?> CreateProductAsync(ProductDto productDto)
    {
        if (!string.IsNullOrEmpty(productDto.Image))
        {
            var extension = GetImageExtension(productDto.Image);
            var imageName = $"{Guid.NewGuid()}{extension}";
            SaveImage(productDto.Image, imageName);
            productDto.Image = imageName;
        }

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

        if (product is null)
            return null;

        var imagePath = Path.Combine(ImgFolderPath, product.Image);
        var ret = mapper.Map<ProductDetailDto>(product);

        if (!File.Exists(imagePath))
            return ret;

        var image = await File.ReadAllBytesAsync(imagePath);
        ret.Image = Convert.ToBase64String(image);

        return ret;
    }

    public async Task<ProductDto?> UpdateProductAsync(ProductDto productDto)
    {
        var oldProduct = await productRepository.GetByIdAsync(productDto.Id);
        if (oldProduct is null)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(productDto.Image))
        {
            if (oldProduct.Image != null)
            {
                var oldImagePath = Path.Combine(ImgFolderPath, oldProduct.Image);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            var extension = GetImageExtension(productDto.Image);
            var imageName = $"{Guid.NewGuid()}{extension}";
            SaveImage(productDto.Image, imageName);
            productDto.Image = imageName;
        }

        var updatedProduct = await productRepository.UpdateAsync(mapper.Map<Product>(productDto));
        return updatedProduct is null ? null : mapper.Map<ProductDto>(updatedProduct);
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        return await productRepository.DeleteAsync(id);
    }
}
