using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class AddressController(IAddressService addressService) : ControllerBase
{
    private const string ApiBaseName = "Address";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateAddress))]
    public async Task<ActionResult<AddressDto>> CreateAddress(AddressDto address)
    {
        var result = await addressService.CreateAddressAsync(address);
        return result == null ? Problem() : Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllAddresses))]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAllAddresses()
    {
        var result = await addressService.GetAllAddressesAsync();
        return Ok(result);
    }

    [HttpGet("{addressId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetAddress))]
    public async Task<ActionResult<AddressDto>> GetAddress(int addressId)
    {
        var result = await addressService.GetAddressByIdAsync(addressId);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateAddress))]
    public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
    {
        var result = await addressService.UpdateAddressAsync(address);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{addressId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteAddress))]
    public async Task<ActionResult<bool>> DeleteAddress(int addressId)
    {
        var result = await addressService.DeleteAddressByIdAsync(addressId);
        return result ? Ok() : NotFound();
    }
}
