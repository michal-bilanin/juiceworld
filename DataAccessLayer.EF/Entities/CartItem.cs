using System.ComponentModel.DataAnnotations.Schema;

namespace JuiceWorld.Entities;

public class CartItem : BaseEntity
{
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }

    public virtual Product Product { get; set; }
    public virtual User User { get; set; }
}
