using System.Security.Claims;
using API.Application.Contracts;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using API.Domain.Entities;
using API.Domain.Repositories;

namespace API.Application.Services;

public class PinnedCityService(IUserService userService, IPinnedCityRepository repository) : IPinnedCityService
{
    public async Task<PinnedCityDto> CreateForUserAsync(ClaimsPrincipal principal, CreatePinnedCityDto data)
    {
        var user = await userService.GetUserAsync(principal);

        if (user == null)
        {
            throw new ArgumentException("Could not find user for claims principal.", nameof(principal));
        }
        
        return await CreateForUserAsync(user, data);
    }

    public async Task<PinnedCityDto> CreateForUserAsync(ApplicationUser user, CreatePinnedCityDto data)
    {
        var city = new PinnedCity
        {
            Name = data.Name,
            Latitude = data.Latitude,
            Longitude = data.Longitude,
            User = user,
            UserId = user.Id
        };
        
        if (String.IsNullOrEmpty(city.Name))
        {
            city.Name = "Unknown";
            // TODO Replace this with automatic recognition based on Lat/Lng
        }

        await repository.AddAsync(city);
        await repository.SaveChangesAsync();

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