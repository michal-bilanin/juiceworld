using System.ComponentModel.DataAnnotations;

namespace JuiceWorld.Entities;

public class Manufacturer : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual IEnumerable<Product> Products { get; set; } = null!;
}
