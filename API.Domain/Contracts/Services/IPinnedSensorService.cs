using API.Domain.Dto;
using API.Domain.Entities;
using System.Security.Claims;

namespace API.Domain.Contracts.Services
{
    public interface IPinnedSensorService
    {
        public Task<IEnumerable<PinnedSensorDto>> GetForUserAsync(ClaimsPrincipal principal);
        public Task<IEnumerable<PinnedSensorDto>> GetForUserAsync(ApplicationUser user);
        public Task<PaginatedResultDto<PinnedSensorDto>> GetPaginatedForUserAsync(ClaimsPrincipal principal, PaginationOptionsDto pagination);
        public Task<PaginatedResultDto<PinnedSensorDto>> GetPaginatedForUserAsync(ApplicationUser user, PaginationOptionsDto pagination);

        public Task<PinnedSensorDto> CreateForUserAsync(ClaimsPrincipal principal, CreatePinnedSensorDto data);
        public Task<PinnedSensorDto> CreateForUserAsync(ApplicationUser user, CreatePinnedSensorDto data);

        public Task<PinnedSensorDto?> GetByIdAsync(Guid id);

        public Task<PinnedSensorDto> UpdatePinnedSensorAsync(Guid id, CreatePinnedSensorDto pinnedSensorDto);
        public Task<PinnedSensorDto> UpdatePinnedSensorAsync(PinnedSensorDto sensorDto, CreatePinnedSensorDto pinnedSensorDto);

        public Task DeletePinnedSensorAsync(PinnedSensorDto pinnedSensorDto);
        public Task DeletePinnedSensorAsync(Guid id);

        public Task<bool> UserCanReadSensorAsync(ClaimsPrincipal principal, PinnedSensor resource);
        public Task<bool> UserCanReadSensorAsync(ApplicationUser user, PinnedSensor resource);

        public Task<bool> UserCanUpdateSensorAsync(ClaimsPrincipal principal, PinnedSensor resource);
        public Task<bool> UserCanUpdateSensorAsync(ApplicationUser user, PinnedSensor resource);

        public Task<bool> UserCanDeleteSensorAsync(ClaimsPrincipal principal, PinnedSensor resource);
        public Task<bool> UserCanDeleteSensorAsync(ApplicationUser user, PinnedSensor resource);
    }
}
