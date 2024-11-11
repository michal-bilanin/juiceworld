using Commons.Enums;

namespace BusinessLayer.DTOs;

public class AddressDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
    public AddressType Type { get; set; } = AddressType.Shipping;
    public int UserId { get; set; }
}
