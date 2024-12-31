using BusinessLayer.DTOs;
using Infrastructure.QueryObjects;
using JuiceWorld.Entities;

namespace BusinessLayer.Services.Interfaces;

public interface IGiftCardService
{
    Task<GiftCardDetailDto?> CreateGiftCardAsync(GiftCardCreateDto giftCardCreateDto);
    Task<FilteredResult<GiftCardEditDto>> GetAllGiftCardsAsync(GiftCardFilterDto giftCardFilterDto);
    Task<GiftCardDetailDto?> GetGiftCardByIdAsync(int id);
    Task<GiftCardDetailDto?> UpdateGiftCardAsync(GiftCardEditDto giftCardDto);
    Task<bool> DeleteGiftCardByIdAsync(int id);
    Task<CouponCodeDto?> RedeemCouponAsync(string CouponCode);

    Task<IEnumerable<CouponCodeDto>> GetCouponCodesAsync();

    Task<CouponCodeDto?> GetCouponCodeByIdAsync(int id);

    Task<CouponCodeDto?> GetCouponByCodeAsync(string code);
}