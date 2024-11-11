using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.Repositories;
using JuiceWorld.Entities;

namespace BusinessLayer.Services;

public class AddressService(IRepository<Address> addressRepository, IMapper mapper) : IAddressService
{
    public async Task<AddressDto?> CreateAddressAsync(AddressDto address)
    {
        var newAddress = await addressRepository.CreateAsync(mapper.Map<Address>(address));
        return newAddress is null ? null : mapper.Map<AddressDto>(newAddress);
    }

    public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync()
    {
        var addresses = await addressRepository.GetAllAsync();
        return mapper.Map<List<AddressDto>>(addresses);
    }

    public async Task<AddressDto?> GetAddressByIdAsync(int addressId)
    {
        var address = await addressRepository.GetByIdAsync(addressId);
        return address is null ? null : mapper.Map<AddressDto>(address);
    }

    public async Task<AddressDto?> UpdateAddressAsync(AddressDto address)
    {
        var updatedAddress = await addressRepository.UpdateAsync(mapper.Map<Address>(address));
        return updatedAddress is null ? null : mapper.Map<AddressDto>(updatedAddress);
    }

    public async Task<bool> DeleteAddressByIdAsync(int addressId)
    {
        return await addressRepository.DeleteAsync(addressId);
    }
}
