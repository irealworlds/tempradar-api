using System.Linq.Expressions;
using System.Security.Claims;
using API.Application.Contracts;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using API.Domain.Entities;
using API.Domain.Repositories;
using AutoMapper;

namespace API.Application.Services;

public class PinnedCityService
    (IUserService userService, IPinnedCityRepository repository, IMapper mapper) : IPinnedCityService
{
    public async Task<IEnumerable<PinnedCityDto>> GetForUserAsync(ClaimsPrincipal principal)
    {
        var user = await userService.GetUserAsync(principal);

        if (user == null) throw new ArgumentException("Could not find user for claims principal.", nameof(principal));

        return await this.GetForUserAsync(user);
    }

    public async Task<IEnumerable<PinnedCityDto>> GetForUserAsync(ApplicationUser user)
    {
        var cities = await repository.GetAllAsync(new List<Expression<Func<PinnedCity, bool>>>
        {
            city => city.UserId.Equals(user.Id)
        });

        return cities.Select(mapper.Map<PinnedCityDto>);
    }

    public async Task<PaginatedResultDto<PinnedCityDto>> GetPaginatedForUserAsync(ClaimsPrincipal principal,
        PaginationOptionsDto pagination)
    {
        var user = await userService.GetUserAsync(principal);

        if (user == null) throw new ArgumentException("Could not find user for claims principal.", nameof(principal));

        return await this.GetPaginatedForUserAsync(user, pagination);
    }

    public async Task<PaginatedResultDto<PinnedCityDto>> GetPaginatedForUserAsync(ApplicationUser user,
        PaginationOptionsDto pagination)
    {
        var cityFilterExpressions = new List<Expression<Func<PinnedCity, bool>>>
        {
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
            Items = cities.Select(mapper.Map<PinnedCityDto>),
            Total = total
        };
    }

    public async Task<PinnedCityDto> CreateForUserAsync(ClaimsPrincipal principal, CreatePinnedCityDto data)
    {
        var user = await userService.GetUserAsync(principal);

        if (user == null) throw new ArgumentException("Could not find user for claims principal.", nameof(principal));

        return await this.CreateForUserAsync(user, data);
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

        if (string.IsNullOrEmpty(city.Name)) city.Name = "Unknown";
        // TODO Replace this with automatic recognition based on Lat/Lng
        await repository.AddAsync(city);
        await repository.SaveChangesAsync();

        return mapper.Map<PinnedCityDto>(city);
    }

    public async Task<PinnedCityDto?> GetByIdAsync(Guid id)
    {
        var city = await repository.GetByIdAsync(id);

        return city == null ? null : mapper.Map<PinnedCityDto>(city);
    }

    public async Task<PinnedCityDto> UpdatePinnedCityAsync(Guid id, CreatePinnedCityDto pinnedCityDto)
    {
        var city = await repository.GetByIdAsync(id);

        if (city == null) throw new ArgumentException("Could not find pinned city by id: " + id, nameof(id));

        return await this.UpdatePinnedCityAsync(mapper.Map<PinnedCityDto>(city), pinnedCityDto);
    }

    public async Task<PinnedCityDto> UpdatePinnedCityAsync(PinnedCityDto cityDto, CreatePinnedCityDto pinnedCityDto)
    {
        var city = await repository.GetByIdAsync(cityDto.Id);

        if (city == null) throw new ArgumentException("City not found", nameof(cityDto));

        city.Name = pinnedCityDto.Name;
        city.Longitude = pinnedCityDto.Longitude;
        city.Latitude = pinnedCityDto.Latitude;

        await repository.UpdateAsync(city);
        await repository.SaveChangesAsync();

        return mapper.Map<PinnedCityDto>(city);
    }

    public async Task DeletePinnedCityAsync(PinnedCityDto pinnedCityDto)
    {
        await this.DeletePinnedCityAsync(pinnedCityDto.Id);
    }

    public async Task DeletePinnedCityAsync(Guid id)
    {
        var pinnedCityToDelete = await repository.GetByIdAsync(id);

        if (pinnedCityToDelete == null)
            throw new ArgumentException("Could not find pinned city by id: " + id, nameof(id));

        await repository.DeleteAsync(pinnedCityToDelete);
        await repository.SaveChangesAsync();
    }

    public async Task<bool> UserCanReadCityAsync(ClaimsPrincipal principal, PinnedCityDto resource)
    {
        var user = await userService.GetUserAsync(principal);

        if (user == null) throw new ArgumentException("Could not find user for claims principal.", nameof(principal));

        return await this.UserCanReadCityAsync(user, resource);
    }

    public async Task<bool> UserCanReadCityAsync(ApplicationUser user, PinnedCityDto resource)
    {
        var city = await repository.GetByIdAsync(resource.Id);

        return city != null && city.UserId.Equals(user.Id);
    }

    public async Task<bool> UserCanUpdateCityAsync(ClaimsPrincipal principal, PinnedCityDto resource)
    {
        var user = await userService.GetUserAsync(principal);

        if (user == null) throw new ArgumentException("Could not find user for claims principal.", nameof(principal));

        return await this.UserCanUpdateCityAsync(user, resource);
    }

    public async Task<bool> UserCanUpdateCityAsync(ApplicationUser user, PinnedCityDto resource)
    {
        var city = await repository.GetByIdAsync(resource.Id);

        return city != null && city.UserId.Equals(user.Id);
    }

    public async Task<bool> UserCanDeleteCityAsync(ClaimsPrincipal principal, PinnedCityDto resource)
    {
        var user = await userService.GetUserAsync(principal);

        if (user == null) throw new ArgumentException("Could not find user for claims principal.", nameof(principal));

        return await this.UserCanDeleteCityAsync(user, resource);
    }

    public async Task<bool> UserCanDeleteCityAsync(ApplicationUser user, PinnedCityDto resource)
    {
        var city = await repository.GetByIdAsync(resource.Id);

        return city != null && city.UserId.Equals(user.Id);
    }
}