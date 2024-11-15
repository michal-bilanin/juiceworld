using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Customer))]
public class ProductController(IProductService productService) : ControllerBase
{
    private const string ApiBaseName = "Product";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateProduct))]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto product)
    {
        var result = await productService.CreateProductAsync(product);
        return result == null ? Problem() : Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetProductFiltered))]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductFiltered(
        [FromQuery] ProductFilterDto productFilter)
    {
        var result = await productService.GetProductsFilteredAsync(productFilter);
        return Ok(result);
    }

    [HttpGet("{productId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetProduct))]
    public async Task<ActionResult<ProductDto>> GetProduct(int productId)
    {
        var result = await productService.GetProductByIdAsync(productId);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateProduct))]
    public async Task<ActionResult<ProductDto>> UpdateProduct(ProductDto product)
    {
        var result = await productService.UpdateProductAsync(product);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{productId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteProduct))]
    public async Task<ActionResult<bool>> DeleteProduct(int productId)
    {
        var result = await productService.DeleteProductByIdAsync(productId);
        return result ? Ok() : NotFound();
    }
}
