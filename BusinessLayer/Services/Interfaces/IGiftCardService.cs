using BusinessLayer.DTOs;
using JuiceWorld.Entities;

namespace BusinessLayer.Services.Interfaces;

public interface IGiftCardService
{
    Task<GiftCardDetailDto?> CreateGfitCardAsync(GiftCardCreateDto gfitCardCreateDto);
    Task<IEnumerable<GiftCardEditDto>> GetAllGfitCardsAsync();
    Task<GiftCardDetailDto?> GetGfitCardByIdAsync(int id);
    Task<GiftCardDetailDto?> UpdateGfitCardAsync(GiftCardEditDto GfitCardDto);
    Task<bool> DeleteGfitCardByIdAsync(int id);

    Task<CouponCodeDto?> RedeemCouponCodeAsync(string couponCode);

    Task<IEnumerable<CouponCodeDto>> GetCouponCodesAsync();

    Task<CouponCodeDto?> GetCouponCodeByIdAsync(int id);

    Task<CouponCodeDto?> GetCouponByCodeAsync(string code);
}