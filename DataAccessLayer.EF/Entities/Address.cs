using JuiceWorld.Enums;

namespace JuiceWorld.Entities;

public class Address : BaseEntity
{
    public string Name { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public AddressType Type { get; set; } = AddressType.Shipping;
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public virtual IEnumerable<Order> Orders { get; set; }
}
