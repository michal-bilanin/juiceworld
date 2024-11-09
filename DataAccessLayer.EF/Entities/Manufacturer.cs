namespace JuiceWorld.Entities;

public class Manufacturer : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual IEnumerable<Product> Products { get; set; } = null!;
}
