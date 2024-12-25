using Commons.Enums;

namespace BusinessLayer.DTOs;

public class AddressDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public AddressType Type { get; set; } = AddressType.Shipping;
    public int UserId { get; set; }
}
