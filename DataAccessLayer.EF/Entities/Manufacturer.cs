using System.ComponentModel.DataAnnotations;
using JuiceWorld.Entities.Interfaces;

namespace JuiceWorld.Entities;

public class Manufacturer : BaseEntity, IAuditableEntity
{
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual IEnumerable<Product> Products { get; set; } = null!;
}
