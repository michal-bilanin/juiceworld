using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using JuiceWorld.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Customer))]
public class GiftCardController(IGiftCardService GfitCardService) : ControllerBase
{
    private const string ApiBaseName = "GfitCard";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateGfitCard))]
    public async Task<ActionResult<GiftCardDetailDto>> CreateGfitCard(GiftCardCreateDto gfitCardCreate)
    {
        var result = await GfitCardService.CreateGfitCardAsync(gfitCardCreate);
        return result == null ? Problem() : Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllGfitCards))]
    public async Task<ActionResult<IEnumerable<GiftCardEditDto>>> GetAllGfitCards()
    {
        var result = await GfitCardService.GetAllGfitCardsAsync();
        return Ok(result);
    }

    [HttpGet("{GfitCardId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetGfitCard))]
    public async Task<ActionResult<GiftCardDetailDto>> GetGfitCard(int GfitCardId)
    {
        var result = await GfitCardService.GetGfitCardByIdAsync(GfitCardId);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateGfitCard))]
    public async Task<ActionResult<GiftCardDetailDto>> UpdateGfitCard(GiftCardEditDto gfitCard)
    {
        var result = await GfitCardService.UpdateGfitCardAsync(gfitCard);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{GfitCardId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteGfitCard))]
    public async Task<ActionResult<bool>> DeleteGfitCard(int GfitCardId)
    {
        var result = await GfitCardService.DeleteGfitCardByIdAsync(GfitCardId);
        return result ? Ok() : NotFound();
    }
    
    [HttpPost("redeem")]
    [OpenApiOperation(ApiBaseName + nameof(RedeemCouponCode))]
    public async Task<ActionResult<CouponCode>> RedeemCouponCode(string couponCode)
    {
        var result = await GfitCardService.RedeemCouponCodeAsync(couponCode);
        return result == null ? NotFound() : Ok(result);
    }
    
    [HttpGet("coupon-codes")]
    [OpenApiOperation(ApiBaseName + nameof(GetCouponCodes))]
    public async Task<ActionResult<IEnumerable<CouponCodeDto>>> GetCouponCodes()
    {
        var result = await GfitCardService.GetCouponCodesAsync();
        return Ok(result);
    }
    
    [HttpGet("coupon-codes/{couponCode}")]
    [OpenApiOperation(ApiBaseName + nameof(GetCouponCode))]
    public async Task<ActionResult<CouponCodeDto>> GetCouponCode(string couponCode)
    {
        var result = await GfitCardService.GetCouponByCodeAsync(couponCode);
        return result == null ? NotFound() : Ok(result);
    }
}