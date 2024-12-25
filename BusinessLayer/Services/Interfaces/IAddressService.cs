using BusinessLayer.DTOs;

namespace BusinessLayer.Services.Interfaces;

public interface IAddressService
{
    public Task<AddressDto?> CreateAddressAsync(AddressDto addressDto);
    public Task<IEnumerable<AddressDto>> GetAllAddressesAsync();
    public Task<AddressDto?> GetAddressByIdAsync(int id);
    public Task<AddressDto?> UpdateAddressAsync(AddressDto addressDto);
    public Task<bool> DeleteAddressByIdAsync(int id);
}
