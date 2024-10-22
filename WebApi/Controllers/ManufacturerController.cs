using AutoMapper;
using Infrastructure.UnitOfWork;
using JuiceWorld.Entities;
using JuiceWorld.Enums;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class ManufacturerController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "Manufacturer";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateManufacturer))]
    public async Task<ActionResult<ManufacturerDto>> CreateManufacturer(ManufacturerDto manufacturer)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ManufacturerRepository.Create(mapper.Map<Manufacturer>(manufacturer));
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(mapper.Map<ManufacturerDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllManufacturers))]
    public async Task<ActionResult<List<ManufacturerDto>>> GetAllManufacturers()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ManufacturerRepository.GetAll();
        return Ok(mapper.Map<ICollection<ManufacturerDto>>(result).ToList());
    }

    [HttpGet("{manufacturerId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetManufacturer))]
    public async Task<ActionResult<ManufacturerDto>> GetManufacturer(int manufacturerId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ManufacturerRepository.GetById(manufacturerId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<ManufacturerDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateManufacturer))]
    public async Task<ActionResult<ManufacturerDto>> UpdateManufacturer(ManufacturerDto manufacturer)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.ManufacturerRepository.Update(mapper.Map<Manufacturer>(manufacturer));
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(mapper.Map<ManufacturerDto>(result));
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
