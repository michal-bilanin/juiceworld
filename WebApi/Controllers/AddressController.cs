using AutoMapper;
using BusinessLayer.DTOs;
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
public class AddressController(IRepository<Address> addressRepository, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "Address";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateAddress))]
    public async Task<ActionResult<AddressDto>> CreateAddress(AddressDto address)
    {
        var result = await addressRepository.CreateAsync(mapper.Map<Address>(address));
        return result == null ? Problem() : Ok(mapper.Map<AddressDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllAddresses))]
    public async Task<ActionResult<List<AddressDto>>> GetAllAddresses()
    {
        var result = await addressRepository.GetAllAsync();
        return Ok(mapper.Map<ICollection<AddressDto>>(result).ToList());
    }

    [HttpGet("{addressId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetAddress))]
    public async Task<ActionResult<AddressDto>> GetAddress(int addressId)
    {
        var result = await addressRepository.GetByIdAsync(addressId);
        return result == null ? NotFound() : Ok(mapper.Map<AddressDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateAddress))]
    public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
    {
        var result = await addressRepository.UpdateAsync(mapper.Map<Address>(address));
        return result == null ? Problem() : Ok(mapper.Map<AddressDto>(result));
    }

    [HttpDelete("{addressId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteAddress))]
    public async Task<ActionResult<bool>> DeleteAddress(int addressId)
    {
        var result = await addressRepository.DeleteAsync(addressId);
        return result ? Ok() : NotFound();
    }
}
