using API.Application.Contracts;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using API.Domain.Entities;
using API.Domain.Repositories;
using System.Linq.Expressions;
using System.Security.Claims;

namespace API.Application.Services;

public class PinnedCityService(IUserService userService, IPinnedCityRepository repository) : IPinnedCityService
{
    public async Task<IEnumerable<PinnedCityDto>> GetForUserAsync(ClaimsPrincipal principal)
    {
        var user = await userService.GetUserAsync(principal);

        if (user == null)
        {
            throw new ArgumentException("Could not find user for claims principal.", nameof(principal));
        }

        return await GetForUserAsync(user);
    }

    public async Task<IEnumerable<PinnedCityDto>> GetForUserAsync(ApplicationUser user)
    {
        var cities = await repository.GetAllAsync(new List<Expression<Func<PinnedCity, bool>>>
        {
            city => city.UserId.Equals(user.Id)
        });
        return cities.Select(city => new PinnedCityDto
        {
            Id = city.Id,
            Name = city.Name,
            Latitude = city.Latitude,
            Longitude = city.Longitude,
            CreatedAt = city.CreatedAt,
        });
    }

    public async Task<PaginatedResultDto<PinnedCityDto>> GetPaginatedForUserAsync(ClaimsPrincipal principal, PaginationOptionsDto pagination)
    {
        var user = await userService.GetUserAsync(principal);

        if (user == null)
        {
            throw new ArgumentException("Could not find user for claims principal.", nameof(principal));
        }

        return await GetPaginatedForUserAsync(user, pagination);
    }

    public async Task<PaginatedResultDto<PinnedCityDto>> GetPaginatedForUserAsync(ApplicationUser user, PaginationOptionsDto pagination)
    {
        var cityFilterExpressions = new List<Expression<Func<PinnedCity, bool>>> {
            city => city.UserId.Equals(user.Id)
        };

        var cities = await repository.GetAllAsync(
            pagination.Skip,
            pagination.Limit,
            cityFilterExpressions
        );
        var total = await repository.Count(cityFilterExpressions);

        return new PaginatedResultDto<PinnedCityDto>
        {
            Items = cities.Select(city => new PinnedCityDto
            {
                Id = city.Id,
                Name = city.Name,
                Latitude = city.Latitude,
                Longitude = city.Longitude,
                CreatedAt = city.CreatedAt,
            }),
            Total = total
        };
    }

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
            CreatedAt = city.CreatedAt,
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
            CreatedAt = city.CreatedAt,
        };
    }

    public async Task UpdatePinnedCityAsync(Guid id, CreatePinnedCityDto pinnedCityDto)
    {
        var pinnedCityToUpdate = await repository.GetByIdAsync(id);

        if (pinnedCityToUpdate != null)
        {
            pinnedCityToUpdate.Name = pinnedCityDto.Name;
            pinnedCityToUpdate.Longitude = pinnedCityDto.Longitude;
            pinnedCityToUpdate.Latitude = pinnedCityDto.Latitude;

            await repository.UpdateAsync(pinnedCityToUpdate);
            await repository.SaveChangesAsync();
        }
    }

    public async Task DeletePinnedCityByIdAsync(Guid id)
    {
        var pinnedCityToDelete = await repository.GetByIdAsync(id);

        if (pinnedCityToDelete != null)
        {
            await repository.DeleteAsync(pinnedCityToDelete);
            await repository.SaveChangesAsync();
        }
    }
}