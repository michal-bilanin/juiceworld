using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Commons.Enums;

namespace JuiceWorld.Entities;

public class Address : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string City { get; set; } = null!;

    [MaxLength(100)]
    public string Street { get; set; } = null!;

    [MaxLength(10)]
    public string HouseNumber { get; set; } = null!;

    [MaxLength(10)]
    public string ZipCode { get; set; } = null!;

    [MaxLength(100)]
    public string Country { get; set; } = null!;

    public AddressType Type { get; set; } = AddressType.Shipping;
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;

    public virtual IEnumerable<Order> Orders { get; set; } = null!;
}
