using System.ComponentModel.DataAnnotations.Schema;
using Commons.Enums;

namespace JuiceWorld.Entities;

public class Address : BaseEntity
{
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
    public AddressType Type { get; set; } = AddressType.Shipping;
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;

    public virtual IEnumerable<Order> Orders { get; set; } = null!;
}
