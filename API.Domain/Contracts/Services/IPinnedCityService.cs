using System.Security.Claims;
using API.Domain.Dto;
using API.Domain.Entities;

namespace API.Domain.Contracts.Services;

public interface IPinnedCityService
{
    public Task<PinnedCityDto> CreateForUserAsync(ClaimsPrincipal principal, CreatePinnedCityDto data);
    public Task<PinnedCityDto> CreateForUserAsync(ApplicationUser user, CreatePinnedCityDto data);
    public Task<PinnedCityDto?> GetByIdAsync(Guid id);
}