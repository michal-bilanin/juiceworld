using Infrastructure.UnitOfWork;
using JuiceWorld.Entities;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "user")]
public class AddressController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider) : ControllerBase
{
    private const string ApiBaseName = "Address";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateAddress))]
    public async Task<ActionResult<Address>> CreateAddress(Address address)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.AddressRepository.Create(address);
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllAddresses))]
    public async Task<ActionResult<List<Address>>> GetAllAddresses()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.AddressRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{addressId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetAddress))]
    public async Task<ActionResult<Address>> GetAddress(int addressId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.AddressRepository.GetById(addressId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateAddress))]
    public async Task<ActionResult<Address>> UpdateAddress(Address address)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        if (!await unitOfWork.AddressRepository.Update(address))
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
