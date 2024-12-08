using System.ComponentModel.DataAnnotations;
using JuiceWorld.Entities.Interfaces;

namespace JuiceWorld.Entities;

public class Manufacturer : BaseEntity, IAuditableEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }

    public virtual IEnumerable<Product> Products { get; set; } = [];
}
