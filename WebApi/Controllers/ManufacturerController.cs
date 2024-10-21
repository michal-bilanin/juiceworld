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
public class ManufacturerController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider) : ControllerBase
{
    private const string ApiBaseName = "Manufacturer";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateManufacturer))]
    public async Task<ActionResult<Manufacturer>> CreateManufacturer(Manufacturer manufacturer)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ManufacturerRepository.Create(manufacturer);
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllManufacturers))]
    public async Task<ActionResult<List<Manufacturer>>> GetAllManufacturers()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ManufacturerRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{manufacturerId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetManufacturer))]
    public async Task<ActionResult<Manufacturer>> GetManufacturer(int manufacturerId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ManufacturerRepository.GetById(manufacturerId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateManufacturer))]
    public async Task<ActionResult<Manufacturer>> UpdateManufacturer(Manufacturer manufacturer)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        if (!await unitOfWork.ManufacturerRepository.Update(manufacturer))
        {
            return NotFound();
        }

        await unitOfWork.Commit();
        return Ok(manufacturer);
    }

    [HttpDelete("{manufacturerId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteManufacturer))]
    public async Task<ActionResult<bool>> DeleteManufacturer(int manufacturerId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ManufacturerRepository.Delete(manufacturerId);
        if (!result)
        {
            return NotFound();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }
}
