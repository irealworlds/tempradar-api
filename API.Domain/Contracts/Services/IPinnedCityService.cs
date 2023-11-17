using API.Domain.Dto;

namespace API.Domain.Contracts.Services;

public interface IPinnedCityService
{
    public Task<PinnedCityDto> CreateAsync(CreatePinnedCityDto data);
    public Task<PinnedCityDto?> GetByIdAsync(Guid id);
}