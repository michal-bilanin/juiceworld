namespace BusinessLayer.DTOs;

public class GiftCardDetailDto : GiftCardViewDto
{
    public int Id { get; set; }
    public List<CouponCodeDto> CouponCodes { get; set; } = [];
}