using System.Text.Json;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Services;

public class ManufacturerService(
    IRepository<Manufacturer> manufacturerRepository,
    IQueryObject<Manufacturer> manufacturerQueryObject,
    IMemoryCache memoryCache,
    IMapper mapper) : IManufacturerService
{
    private string _cacheKeyPrefix = nameof(ManufacturerService);
    public async Task<ManufacturerDto?> CreateManufacturerAsync(ManufacturerDto manufacturerDto)
    {
        var newManufacturer = await manufacturerRepository.CreateAsync(mapper.Map<Manufacturer>(manufacturerDto));
        return newManufacturer is null ? null : mapper.Map<ManufacturerDto>(newManufacturer);
    }

    public async Task<IEnumerable<ManufacturerDto>> GetAllManufacturersAsync()
    {
        string cacheKey = $"{_cacheKeyPrefix}-allManufacturers";
        if (!memoryCache.TryGetValue(cacheKey, out IEnumerable<Manufacturer>? value))
        {
            value = await manufacturerRepository.GetAllAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            memoryCache.Set(cacheKey, value, cacheEntryOptions);
        }

        return mapper.Map<List<ManufacturerDto>>(value);
    }

    public async Task<FilteredResult<ManufacturerDto>> GetManufacturersAsync(
        ManufacturerFilterDto manufacturerFilterDto)
    {
        var query = manufacturerQueryObject
            .Filter(m => manufacturerFilterDto.Name == null ||
                         m.Name.ToLower().Contains(manufacturerFilterDto.Name.ToLower()))
            .Paginate(manufacturerFilterDto.PageIndex, manufacturerFilterDto.PageSize)
            .OrderBy(m => m.Id);

        var value = await query.ExecuteAsync();


        return new FilteredResult<ManufacturerDto>
        {
            Entities = mapper.Map<List<ManufacturerDto>>(value.Entities),
            PageIndex = value.PageIndex,
            TotalPages = value.TotalPages
        };
    }

    public async Task<ManufacturerDto?> GetManufacturerByIdAsync(int id)
    {
        string cacheKey = $"{_cacheKeyPrefix}-Manufacturer{id}";
        if (!memoryCache.TryGetValue(cacheKey, out Manufacturer? value))
        {
            value = await manufacturerRepository.GetByIdAsync(id);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            memoryCache.Set(cacheKey, value, cacheEntryOptions);
        }

        return value is null ? null : mapper.Map<ManufacturerDto>(value);
    }

    public async Task<ManufacturerDto?> UpdateManufacturerAsync(ManufacturerDto manufacturerDto)
    {
        string cacheKey = $"{_cacheKeyPrefix}-Manufacturer{manufacturerDto.Id}";
        memoryCache.Remove(cacheKey);
        var updatedManufacturer = await manufacturerRepository.UpdateAsync(mapper.Map<Manufacturer>(manufacturerDto));
        return updatedManufacturer is null ? null : mapper.Map<ManufacturerDto>(updatedManufacturer);
    }

    public async Task<bool> DeleteManufacturerByIdAsync(int id)
    {
        string cacheKey = $"{_cacheKeyPrefix}-Manufacturer{id}";
        memoryCache.Remove(cacheKey);
        return await manufacturerRepository.DeleteAsync(id);
    }
}
