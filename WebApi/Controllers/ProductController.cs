using AutoMapper;
using Infrastructure.QueryObjects;
using Infrastructure.UnitOfWork;
using JuiceWorld.Entities;
using JuiceWorld.Enums;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class ProductController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider, IMapper mapper, IQueryObject<Product> queryObject) : ControllerBase
{
    private const string ApiBaseName = "Product";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateProduct))]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto product)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ProductRepository.Create(mapper.Map<Product>(product));
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(mapper.Map<ProductDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetProductByManufacturer))]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByManufacturer([FromQuery] ProductFilterDto productFilter)
    {
        Enum.TryParse<ProductCategory>(productFilter.Category, true, out var categoryEnum);
        var result = await queryObject.Filter(p =>
            // productFilter.MmanufacturerName?.contains(...) ?? true;
            (productFilter.MmanufacturerName == null || p.Manufacturer.Name.ToLower().Contains(productFilter.MmanufacturerName.ToLower())) &&
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
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ProductRepository.GetById(productId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<ProductDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateProduct))]
    public async Task<ActionResult<ProductDto>> UpdateProduct(ProductDto product)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        if (!await unitOfWork.ProductRepository.Update(mapper.Map<Product>(product)))
        {
            return NotFound();
        }

        await unitOfWork.Commit();
        return Ok(product);
    }

    [HttpDelete("{productId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteProduct))]
    public async Task<ActionResult<bool>> DeleteProduct(int productId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ProductRepository.Delete(productId);
        if (!result)
        {
            return NotFound();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }
}
