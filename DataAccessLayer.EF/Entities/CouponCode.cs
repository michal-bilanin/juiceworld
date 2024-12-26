using System.ComponentModel.DataAnnotations.Schema;
using JuiceWorld.Entities.Interfaces;

namespace JuiceWorld.Entities;

public class CouponCode: BaseEntity, IAuditableEntity
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public int GiftCardId { get; set; }
    public DateTime? RedeemedAt { get; set; }
    
    [ForeignKey(nameof(GiftCardId))]
    public virtual GiftCard? GiftCard { get; set; }
}
