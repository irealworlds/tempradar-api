using API.Domain.Contracts.Services;
using API.Domain.Dto;
using API.Domain.Entities;
using API.Domain.Repositories;

namespace API.Application.Services;

public class PinnedCityService(IPinnedCityRepository repository) : IPinnedCityService
{
    public async Task<PinnedCityDto> CreateAsync(CreatePinnedCityDto data)
    {
        var city = new PinnedCity()
        {
            Name = data.Name,
            Latitude = data.Latitude,
            Longitude = data.Longitude
        };
        
        if (String.IsNullOrEmpty(city.Name))
        {
            city.Name = "Unknown";
            // TODO Replace this with automatic recognition based on Lat/Lng
        }

        await repository.AddAsync(city);

        return new PinnedCityDto
        {
            Id = city.Id,
            Name = city.Name,
            Latitude = city.Latitude,
            Longitude = city.Longitude,
        };
    }

    public async Task<PinnedCityDto?> GetByIdAsync(Guid id)
    {
        var city = await repository.GetByIdAsync(id);
        
        if (city == null)
        {
            return null;
        }
        
        return new PinnedCityDto
        {
            Id = city.Id,
            Name = city.Name,
            Latitude = city.Latitude,
            Longitude = city.Longitude,
        };
    }
}