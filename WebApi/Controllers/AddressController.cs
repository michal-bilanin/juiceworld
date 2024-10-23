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
public class AddressController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "Address";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateAddress))]
    public async Task<ActionResult<AddressDto>> CreateAddress(AddressDto address)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.AddressRepository.Create(mapper.Map<Address>(address));
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(mapper.Map<AddressDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllAddresses))]
    public async Task<ActionResult<List<AddressDto>>> GetAllAddresses()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.AddressRepository.GetAll();
        return Ok(mapper.Map<ICollection<AddressDto>>(result).ToList());
    }

    [HttpGet("{addressId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetAddress))]
    public async Task<ActionResult<AddressDto>> GetAddress(int addressId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.AddressRepository.GetById(addressId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<AddressDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateAddress))]
    public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        if (!await unitOfWork.AddressRepository.Update(mapper.Map<Address>(address)))
        {
            return NotFound();
        }

        await unitOfWork.Commit();
        return Ok(address);
    }

    [HttpDelete("{addressId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteAddress))]
    public async Task<ActionResult<bool>> DeleteAddress(int addressId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.AddressRepository.Delete(addressId);
        if (!result)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }
}
