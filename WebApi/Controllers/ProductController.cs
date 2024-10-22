using Infrastructure.UnitOfWork;
using JuiceWorld.Entities;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "admin")]
public class ProductController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider) : ControllerBase
{
    private const string ApiBaseName = "Product";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateProduct))]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ProductRepository.Create(product);
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllProducts))]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ProductRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{productId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetProduct))]
    public async Task<ActionResult<Product>> GetProduct(int productId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ProductRepository.GetById(productId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateProduct))]
    public async Task<ActionResult<Product>> UpdateProduct(Product product)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        if (!await unitOfWork.ProductRepository.Update(product))
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
