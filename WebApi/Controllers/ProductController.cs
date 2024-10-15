using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private const string ApiBaseName = "Product";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateProduct))]
    public async Task<ActionResult<bool>> CreateProduct()
    {
        return Problem();
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllProducts))]
    public async Task<ActionResult<bool>> GetAllProducts()
    {
        return Problem();
    }

    [HttpGet("{productId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetProduct))]
    public async Task<ActionResult<bool>> GetProduct(int productId)
    {
        return Problem();
    }

    [HttpPut("{productId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(UpdateProduct))]
    public async Task<ActionResult<bool>> UpdateProduct(int productId)
    {
        return Problem();
    }

    [HttpDelete("{productId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteProduct))]
    public async Task<ActionResult<bool>> DeleteProduct(int productId)
    {
        return Problem();
    }
}