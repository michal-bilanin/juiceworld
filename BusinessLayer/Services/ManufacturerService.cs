using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.Repositories;
using JuiceWorld.Entities;

namespace BusinessLayer.Services;

public class ManufacturerService(IRepository<Manufacturer> manufacturerRepository, IMapper mapper) : IManufacturerService
{
    public async Task<ManufacturerDto?> CreateManufacturerAsync(ManufacturerDto manufacturerDto)
    {
        var newManufacturer = await manufacturerRepository.CreateAsync(mapper.Map<Manufacturer>(manufacturerDto));
        return newManufacturer is null ? null : mapper.Map<ManufacturerDto>(newManufacturer);
    }

    public async Task<IEnumerable<ManufacturerDto>> GetAllManufacturersAsync()
    {
        var manufacturers = await manufacturerRepository.GetAllAsync();
        return mapper.Map<List<ManufacturerDto>>(manufacturers);
    }

    public async Task<ManufacturerDto?> GetManufacturerByIdAsync(int id)
    {
        var manufacturer = await manufacturerRepository.GetByIdAsync(id);
        return manufacturer is null ? null : mapper.Map<ManufacturerDto>(manufacturer);
    }

    public async Task<ManufacturerDto?> UpdateManufacturerAsync(ManufacturerDto manufacturerDto)
    {
        var updatedManufacturer = await manufacturerRepository.UpdateAsync(mapper.Map<Manufacturer>(manufacturerDto));
        return updatedManufacturer is null ? null : mapper.Map<ManufacturerDto>(updatedManufacturer);
    }

    public async Task<bool> DeleteManufacturerByIdAsync(int id)
    {
        return await manufacturerRepository.DeleteAsync(id);
    }
}
