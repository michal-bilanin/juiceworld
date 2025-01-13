namespace PresentationLayer.Mvc.Models;

public class GiftCardDetailViewModel : GiftCardViewModel
{
    public List<CouponCodeViewModel> CouponCodes { get; set; } = [];
}
