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
        var result = await queryObject.Filter(p =>
            (productFilter.MmanufacturerName == null || p.Manufacturer.Name == productFilter.MmanufacturerName) &&
            (productFilter.Category == null || p.Category == productFilter.Category)
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
