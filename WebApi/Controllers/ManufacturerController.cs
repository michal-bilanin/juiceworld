using AutoMapper;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using JuiceWorld.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class ManufacturerController(IRepository<Manufacturer> manufacturerRepository, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "Manufacturer";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateManufacturer))]
    public async Task<ActionResult<ManufacturerDto>> CreateManufacturer(ManufacturerDto manufacturer)
    {
        var result = await manufacturerRepository.Create(mapper.Map<Manufacturer>(manufacturer));
        return result == null ? Problem() : Ok(mapper.Map<ManufacturerDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllManufacturers))]
    public async Task<ActionResult<List<ManufacturerDto>>> GetAllManufacturers()
    {
        var result = await manufacturerRepository.GetAll();
        return Ok(mapper.Map<ICollection<ManufacturerDto>>(result).ToList());
    }

    [HttpGet("{manufacturerId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetManufacturer))]
    public async Task<ActionResult<ManufacturerDto>> GetManufacturer(int manufacturerId)
    {
        var result = await manufacturerRepository.GetById(manufacturerId);
        return result == null ? NotFound() : Ok(mapper.Map<ManufacturerDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateManufacturer))]
    public async Task<ActionResult<ManufacturerDto>> UpdateManufacturer(ManufacturerDto manufacturer)
    {
        var result = await manufacturerRepository.Update(mapper.Map<Manufacturer>(manufacturer));
        return result == null ? Problem() : Ok(mapper.Map<ManufacturerDto>(result));
    }

    [HttpDelete("{manufacturerId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteManufacturer))]
    public async Task<ActionResult<bool>> DeleteManufacturer(int manufacturerId)
    {
        var result = await manufacturerRepository.Delete(manufacturerId);
        return result ? Ok() : NotFound();
    }
}
