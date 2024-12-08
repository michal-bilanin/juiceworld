using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Commons.Enums;

namespace JuiceWorld.Entities;

public class Address : BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(100)]
    public required string City { get; set; }

    [MaxLength(100)]
    public required string Street { get; set; }

    [MaxLength(10)]
    public required string HouseNumber { get; set; }

    [MaxLength(10)]
    public required string ZipCode { get; set; }

    [MaxLength(100)]
    public required string Country { get; set; }

    public AddressType Type { get; set; } = AddressType.Shipping;
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }

    public virtual IEnumerable<Order> Orders { get; set; } = [];
}
