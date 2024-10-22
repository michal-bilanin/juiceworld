using Infrastructure.QueryObjects;
using Infrastructure.UnitOfWork;
using JuiceWorld.Entities;
using JuiceWorld.Enums;
using JuiceWorld.QueryObjects;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "customer")]
public class ProductFilterController(IQueryObject<Product> queryObject) : ControllerBase
{
    private const string ApiBaseName = "Product";

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetProductNyManufacturer))]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductNyManufacturer([FromQuery] FilterModel filter)
    {
        var result = await queryObject.Filter(p =>
            (filter.MmanufacturerName == null || p.Manufacturer.Name == filter.MmanufacturerName) &&
            (filter.Category == null || p.Category == filter.Category)
            ).Execute();

        return Ok(result);
    }

    
}
