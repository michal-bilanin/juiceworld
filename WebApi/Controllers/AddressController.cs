using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private const string ApiBaseName = "Address";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateAddress))]
    public async Task<ActionResult<bool>> CreateAddress()
    {
        return Problem();
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllAddresses))]
    public async Task<ActionResult<bool>> GetAllAddresses()
    {
        return Problem();
    }

    [HttpGet("{addressId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetAddress))]
    public async Task<ActionResult<bool>> GetAddress(int addressId)
    {
        return Problem();
    }

    [HttpPut("{addressId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(UpdateAddress))]
    public async Task<ActionResult<bool>> UpdateAddress(int addressId)
    {
        return Problem();
    }

    [HttpDelete("{addressId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteAddress))]
    public async Task<ActionResult<bool>> DeleteAddress(int addressId)
    {
        return Problem();
    }
}