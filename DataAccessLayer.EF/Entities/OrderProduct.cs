using System.ComponentModel.DataAnnotations.Schema;

namespace JuiceWorld.Entities;

public class OrderProduct : BaseEntity
{
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey(nameof(OrderId))]
    public virtual Order Order { get; set; } = null!;
}
