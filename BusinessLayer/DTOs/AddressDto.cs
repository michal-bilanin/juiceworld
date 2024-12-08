using Commons.Enums;

namespace BusinessLayer.DTOs;

public class AddressDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string City { get; set; }
    public required string Street { get; set; }
    public required string HouseNumber { get; set; }
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
    public AddressType Type { get; set; } = AddressType.Shipping;
    public int UserId { get; set; }
}
