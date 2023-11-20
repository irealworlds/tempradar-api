using API.Domain.Dto;
using API.Domain.Entities;
using System.Security.Claims;

namespace API.Domain.Contracts.Services;

public interface IPinnedCityService
{
    public Task<IEnumerable<PinnedCityDto>> GetForUserAsync(ClaimsPrincipal principal);
    public Task<IEnumerable<PinnedCityDto>> GetForUserAsync(ApplicationUser user);
    public Task<PaginatedResultDto<PinnedCityDto>> GetPaginatedForUserAsync(ClaimsPrincipal principal, PaginationOptionsDto pagination);
    public Task<PaginatedResultDto<PinnedCityDto>> GetPaginatedForUserAsync(ApplicationUser user, PaginationOptionsDto pagination);

    public Task<PinnedCityDto> CreateForUserAsync(ClaimsPrincipal principal, CreatePinnedCityDto data);
    public Task<PinnedCityDto> CreateForUserAsync(ApplicationUser user, CreatePinnedCityDto data);

    public Task<PinnedCityDto?> GetByIdAsync(Guid id);

    Task UpdatePinnedCity(Guid id, CreatePinnedCityDto pinnedCityDto);
}