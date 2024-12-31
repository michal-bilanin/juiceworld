using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using JuiceWorld.Entities;

namespace BusinessLayer.Services;

public class ManufacturerService(
    IRepository<Manufacturer> manufacturerRepository,
    IQueryObject<Manufacturer> manufacturerQueryObject,
    IMapper mapper) : IManufacturerService
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

    public async Task<FilteredResult<ManufacturerDto>> GetManufacturersAsync(
        ManufacturerFilterDto manufacturerFilterDto)
    {
        var query = manufacturerQueryObject
            .Filter(m => manufacturerFilterDto.Name == null ||
                         m.Name.ToLower().Contains(manufacturerFilterDto.Name.ToLower()))
            .Paginate(manufacturerFilterDto.PageIndex, manufacturerFilterDto.PageSize)
            .OrderBy(m => m.Id);

        var filteredManufacturers = await query.ExecuteAsync();

        return new FilteredResult<ManufacturerDto>
        {
            Entities = mapper.Map<List<ManufacturerDto>>(filteredManufacturers.Entities),
            PageIndex = filteredManufacturers.PageIndex,
            TotalPages = filteredManufacturers.TotalPages
        };
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
