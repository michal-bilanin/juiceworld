using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManufacturerController : ControllerBase
{
    private const string ApiBaseName = "Manufacturer";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateManufacturer))]
    public async Task<ActionResult<bool>> CreateManufacturer()
    {
        return Problem();
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllManufacturers))]
    public async Task<ActionResult<bool>> GetAllManufacturers()
    {
        return Problem();
    }

    [HttpGet("{manufacturerId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetManufacturer))]
    public async Task<ActionResult<bool>> GetManufacturer(int manufacturerId)
    {
        return Problem();
    }

    [HttpPut("{manufacturerId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(UpdateManufacturer))]
    public async Task<ActionResult<bool>> UpdateManufacturer(int manufacturerId)
    {
        return Problem();
    }

    [HttpDelete("{manufacturerId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteManufacturer))]
    public async Task<ActionResult<bool>> DeleteManufacturer(int manufacturerId)
    {
        return Problem();
    }
}