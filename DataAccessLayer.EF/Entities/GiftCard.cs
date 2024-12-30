
using System.ComponentModel.DataAnnotations;
using JuiceWorld.Entities.Interfaces;

namespace JuiceWorld.Entities;

public class GiftCard : BaseEntity, IAuditableEntity
{
    public int Discount { get; set; }
    public int CouponsCount { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? ExpiryDateTime { get; set; }
    public virtual List<CouponCode> CouponCodes { get; set; } = [];
}