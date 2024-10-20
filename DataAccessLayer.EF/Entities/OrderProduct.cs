namespace JuiceWorld.Entities;

public class OrderProduct : BaseEntity
{
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
    public virtual Order Order { get; set; }
}
