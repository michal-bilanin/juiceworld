namespace BusinessLayer.DTOs;

public class GiftCardDetailDto: GiftCardCreateDto
{
    public int Id { get; set; }
    public List<CouponCodeDto> CouponCodes { get; set; } = [];
}