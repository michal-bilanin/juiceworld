using AutoMapper;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using JuiceWorld.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class ProductController(
    IRepository<Product> productRepository,
    IMapper mapper,
    IQueryObject<Product> queryObject) : ControllerBase
{
    private const string ApiBaseName = "Product";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateProduct))]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto product)
    {
        var result = await productRepository.CreateAsync(mapper.Map<Product>(product));
        return result == null ? Problem() : Ok(mapper.Map<ProductDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetProductByManufacturer))]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByManufacturer(
        [FromQuery] ProductFilterDto productFilter)
    {
        Enum.TryParse<ProductCategory>(productFilter.Category, true, out var categoryEnum);
        var result = await queryObject.Filter(p =>
            (productFilter.ManufacturerName == null ||
             p.Manufacturer.Name.ToLower().Contains(productFilter.ManufacturerName.ToLower())) &&
            (productFilter.Category == null || p.Category == categoryEnum) &&
            (productFilter.PriceMax == null || p.Price <= productFilter.PriceMax) &&
            (productFilter.PriceMin == null || p.Price >= productFilter.PriceMin) &&
            (productFilter.Name == null || p.Name.ToLower().Contains(productFilter.Name.ToLower())) &&
            (productFilter.Description == null || p.Description.ToLower().Contains(productFilter.Description.ToLower()))
        ).Execute();

        return Ok(mapper.Map<ICollection<ProductDto>>(result).ToList());
    }

    [HttpGet("{productId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetProduct))]
    public async Task<ActionResult<ProductDto>> GetProduct(int productId)
    {
        var result = await productRepository.GetByIdAsync(productId);
        return result == null ? NotFound() : Ok(mapper.Map<ProductDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateProduct))]
    public async Task<ActionResult<ProductDto>> UpdateProduct(ProductDto product)
    {
        var result = await productRepository.UpdateAsync(mapper.Map<Product>(product));
        return result == null ? Problem() : Ok(mapper.Map<ProductDto>(result));
    }

    [HttpDelete("{productId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteProduct))]
    public async Task<ActionResult<bool>> DeleteProduct(int productId)
    {
        var result = await productRepository.DeleteAsync(productId);
        return result ? Ok() : NotFound();
    }
}
