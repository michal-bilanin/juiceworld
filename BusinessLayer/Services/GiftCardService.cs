using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using JuiceWorld.Entities;

namespace BusinessLayer.Services;

public class GiftCardService(IRepository<GiftCard> giftCardRepository,
    IRepository<CouponCode> couponCodeRepository,
    IQueryObject<CouponCode> couponCodeQueryObject,
    IMapper mapper) : IGiftCardService
{
    public async Task<GiftCardDetailDto?> CreateGfitCardAsync(GiftCardCreateDto giftCardCreateDto)
    {
        var newGiftCard = await giftCardRepository.CreateAsync(mapper.Map<GiftCard>(giftCardCreateDto));

        if (newGiftCard is null)
            return null;

        foreach (var coupon in Enumerable.Range(0, giftCardCreateDto.CouponsCount))
        {
            await couponCodeRepository.CreateAsync(new CouponCode
            {
                Code = Guid.NewGuid().ToString(),
                GiftCardId = newGiftCard.Id
            });
        }

        var newGiftCardAfterInsert = await giftCardRepository.GetByIdAsync(newGiftCard.Id);
        return mapper.Map<GiftCardDetailDto?>(newGiftCardAfterInsert);
    }

    public async Task<IEnumerable<GiftCardEditDto>> GetAllGfitCardsAsync()
    {
        var giftCards = await giftCardRepository.GetAllAsync();
        return mapper.Map<List<GiftCardEditDto>>(giftCards);
    }

    public async Task<GiftCardDetailDto?> GetGfitCardByIdAsync(int id)
    {
        var giftCard = await giftCardRepository.GetByIdAsync(id);
        return giftCard is null ? null : mapper.Map<GiftCardDetailDto>(giftCard);
    }

    public async Task<GiftCardDetailDto?> UpdateGfitCardAsync(GiftCardEditDto gfitCardDto)
    {
        var updatedGiftCard = await giftCardRepository.UpdateAsync(mapper.Map<GiftCard>(gfitCardDto));
        return updatedGiftCard is null ? null : mapper.Map<GiftCardDetailDto>(updatedGiftCard);
    }

    public async Task<bool> DeleteGfitCardByIdAsync(int id)
    {
        return await giftCardRepository.DeleteAsync(id);
    }

    public async Task<CouponCodeDto?> RedeemCouponCodeAsync(string couponCode)
    {
        var redeemedCoupon = (await couponCodeQueryObject.Filter(c => c.Code == couponCode).ExecuteAsync()).Entities.FirstOrDefault();

        if (redeemedCoupon == null)
            return null;

        if (redeemedCoupon.RedeemedAt != null)
            return null;

        redeemedCoupon.RedeemedAt = DateTime.UtcNow;
        var ret = await couponCodeRepository.UpdateAsync(redeemedCoupon);
        return ret is null ? null : mapper.Map<CouponCodeDto>(ret);
    }

    public async Task<IEnumerable<CouponCodeDto>> GetCouponCodesAsync()
    {
        return await couponCodeRepository.GetAllAsync().ContinueWith(task => mapper.Map<List<CouponCodeDto>>(task.Result));
    }

    public async Task<CouponCodeDto?> GetCouponCodeByIdAsync(int id)
    {
        var coupon = await couponCodeRepository.GetByIdAsync(id);
        return coupon is null ? null : mapper.Map<CouponCodeDto>(coupon);
    }

    public async Task<CouponCodeDto?> GetCouponByCodeAsync(string code)
    {
        var coupon = (await couponCodeQueryObject.Filter(c => c.Code == code).ExecuteAsync()).Entities.FirstOrDefault();
        return coupon is null ? null : mapper.Map<CouponCodeDto>(coupon);
    }
}