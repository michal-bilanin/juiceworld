using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services.Interfaces;

public interface IManufacturerService
{
    Task<ManufacturerDto?> CreateManufacturerAsync(ManufacturerDto manufacturerDto);
    Task<IEnumerable<ManufacturerDto>> GetAllManufacturersAsync();
    Task<ManufacturerDto?> GetManufacturerByIdAsync(int id);
    Task<ManufacturerDto?> UpdateManufacturerAsync(ManufacturerDto manufacturerDto);
    Task<bool> DeleteManufacturerByIdAsync(int id);
}
