using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using JuiceWorld.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services;

public class ProductService(
    IRepository<Product> productRepository,
    IMapper mapper,
    ILogger<ProductService> logger,
    ProductUnitOfWork productUnitOfWork,
    IQueryObject<Product> queryObject) : IProductService
{
    private const string ImgFolderPath = "Images";

    private static readonly Dictionary<string, string> MimeTypes = new()
    {
        // enough to determine the image type
        // source https://stackoverflow.com/questions/57976898/how-to-get-mime-type-from-base-64-string
        { "/9j/", ".jpg" },
        { "iVBORw0KGgo", ".png" },
        { "R0lGODlh", ".gif" },
        { "R0lGODdh", ".gif" }
    };

    private async Task<bool> SaveImageAsync(string base64Image, string imageName)
    {
        Directory.CreateDirectory(ImgFolderPath);
        var imageBytes = Convert.FromBase64String(base64Image);
        var filePath = Path.Combine(ImgFolderPath, imageName);
        try
        {
            await File.WriteAllBytesAsync(filePath, imageBytes);
        }
        catch (Exception e)
        {
            logger.LogError($"ERROR - unable to write to file {filePath} \n ERROR-MESSAGE: {e.Message}");
            return false;
        }

        return true;
    }

    private static string GetImageExtension(string base64Image)
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
            if (!await SaveImageAsync(productDto.Image, imageName))
            {
                return null;
            }

            productDto.Image = imageName;
        }

        var product = mapper.Map<Product>(productDto);
        var tags = await productUnitOfWork.TagRepository.GetByIdRangeAsync(productDto.TagIds.Cast<object>());
        product.Tags = tags.ToList();

        var newProduct = await productUnitOfWork.ProductRepository.CreateAsync(product);
        await productUnitOfWork.Commit();
        return newProduct is null ? null : mapper.Map<ProductDto>(newProduct);
    }

    public async Task<ProductDto?> UpdateProductAsync(ProductDto productDto)
    {
        var oldProduct = await productUnitOfWork.ProductRepository.GetByIdAsync(productDto.Id);
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

            if (!await SaveImageAsync(productDto.Image, imageName))
            {
                return null;
            }

            productDto.Image = imageName;
        }

        var product = mapper.Map<Product>(productDto);
        oldProduct.Tags.Clear();

        var tags = await productUnitOfWork.TagRepository.GetByIdRangeAsync(productDto.TagIds.Cast<object>());
        product.Tags = tags.ToList();

        var updatedProduct = await productUnitOfWork.ProductRepository.UpdateAsync(product);
        await productUnitOfWork.Commit();
        return updatedProduct is null ? null : mapper.Map<ProductDto>(updatedProduct);
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

        var ret = mapper.Map<ProductDetailDto>(product);
        if (product.Image is null)
        {
            return ret;
        }

        var imagePath = Path.Combine(ImgFolderPath, product.Image);
        Console.WriteLine(Path.GetFullPath(imagePath));

        if (!File.Exists(imagePath))
        {
            return ret;
        }

        var image = await File.ReadAllBytesAsync(imagePath);
        ret.Image = Convert.ToBase64String(image);

        return ret;
    }

    public async Task<bool> DeleteProductByIdAsync(int id)
    {
        return await productRepository.DeleteAsync(id);
    }
}
