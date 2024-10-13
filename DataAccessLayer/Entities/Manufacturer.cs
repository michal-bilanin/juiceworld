using JuiceWorld.Entities;
using System;

public class Manufacturer: BaseEntity
{
    public string Name { get; set; }
    public virtual IEnumerable<Product> Products { get; set; }
}
