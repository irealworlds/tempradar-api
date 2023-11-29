using API.Application.Contracts;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using API.Domain.Entities;
using API.Domain.Repositories;
using AutoMapper;
using System.Linq.Expressions;
using System.Security.Claims;

namespace API.Application.Services
{
    public class PinnedSensorService(IUserService userService, IPinnedSensorRepository repository, IMapper mapper) : IPinnedSensorService
    {
        public async Task<IEnumerable<PinnedSensorDto>> GetForUserAsync(ClaimsPrincipal principal)
        {
            var user = await userService.GetUserAsync(principal);

            if (user == null)
            {
                throw new ArgumentException("Could not find user for claims principal.", nameof(principal));
            }
            return await GetForUserAsync(user);
        }

        public async Task<IEnumerable<PinnedSensorDto>> GetForUserAsync(ApplicationUser user)
        {
            var sensors = await repository.GetAllAsync(new List<Expression<Func<PinnedSensor, bool>>>
        {
            sensor => sensor.UserId.Equals(user.Id)
        });

            return sensors.Select(mapper.Map<PinnedSensorDto>);
        }

        public async Task<PaginatedResultDto<PinnedSensorDto>> GetPaginatedForUserAsync(ClaimsPrincipal principal, PaginationOptionsDto pagination)
        {
            var user = await userService.GetUserAsync(principal);

            if (user == null)
            {
                throw new ArgumentException("Could not find user for claims principal.", nameof(principal));
            }

            return await GetPaginatedForUserAsync(user, pagination);
        }

        public async Task<PaginatedResultDto<PinnedSensorDto>> GetPaginatedForUserAsync(ApplicationUser user, PaginationOptionsDto pagination)
        {
            var sensorFilterExpressions = new List<Expression<Func<PinnedSensor, bool>>> {
            sensor => sensor.UserId.Equals(user.Id)
        };

            var sensors = await repository.GetAllAsync(
                pagination.Skip,
                pagination.Limit,
                sensorFilterExpressions
            );
            var total = await repository.Count(sensorFilterExpressions);

            return new PaginatedResultDto<PinnedSensorDto>
            {
                Items = sensors.Select(mapper.Map<PinnedSensorDto>),
                Total = total
            };
        }

        public async Task<PinnedSensorDto> CreateForUserAsync(ClaimsPrincipal principal, CreatePinnedSensorDto data)
        {
            var user = await userService.GetUserAsync(principal);

            if (user == null)
            {
                throw new ArgumentException("Could not find user for claims principal.", nameof(principal));
            }

            return await CreateForUserAsync(user, data);
        }

        public async Task<PinnedSensorDto> CreateForUserAsync(ApplicationUser user, CreatePinnedSensorDto data)
        {
            var sensor = new PinnedSensor
            {
                Name = data.Name,
                SensorId = data.SensorId,
                User = user,
                UserId = user.Id,
            };

            if (String.IsNullOrEmpty(sensor.Name))
            {
                sensor.Name = "Unknown";
            }

            await repository.AddAsync(sensor);
            await repository.SaveChangesAsync();

            return mapper.Map<PinnedSensorDto>(sensor);
        }


        public async Task<PinnedSensorDto?> GetByIdAsync(Guid id)
        {
            var sensor = await repository.GetByIdAsync(id);

            return sensor == null ? null : mapper.Map<PinnedSensorDto>(sensor);
        }

        public async Task<PinnedSensorDto> UpdatePinnedSensorAsync(Guid id, CreatePinnedSensorDto pinnedSensorDto)
        {
            var sensor = await repository.GetByIdAsync(id);

            if (sensor == null)
            {
                throw new ArgumentException("Could not find sensor with given id:" + id, nameof(id));
            }

            return await UpdatePinnedSensorAsync(mapper.Map<PinnedSensorDto>(sensor), pinnedSensorDto);
        }

        public async Task<PinnedSensorDto> UpdatePinnedSensorAsync(PinnedSensorDto sensorDto, CreatePinnedSensorDto pinnedSensorDto)
        {
            var sensor = await repository.GetByIdAsync(sensorDto.Id);

            if (sensor == null)
            {
                throw new ArgumentException("Sensor not found", nameof(sensorDto));
            }

            sensor.Name = pinnedSensorDto.Name;

            await repository.UpdateAsync(sensor);
            await repository.SaveChangesAsync();

            return mapper.Map<PinnedSensorDto>(sensor);
        }

        public async Task DeletePinnedSensorAsync(PinnedSensorDto pinnedSensorDto)
        {
            await DeletePinnedSensorAsync(pinnedSensorDto.Id);
        }

        public async Task DeletePinnedSensorAsync(Guid id)
        {
            var pinnedSensorToDelete = await repository.GetByIdAsync(id);

            if (pinnedSensorToDelete == null)
            {
                throw new ArgumentException("Could not find pinned sensor by id: " + id, nameof(id));
            }

            await repository.DeleteAsync(pinnedSensorToDelete);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> UserCanReadSensorAsync(ClaimsPrincipal principal, PinnedSensor resource)
        {
            var user = await userService.GetUserAsync(principal);

            if (user == null)
            {
                throw new ArgumentException("Could not find user for claims principal.", nameof(principal));
            }

            return await UserCanReadSensorAsync(user, resource);
        }

        public Task<bool> UserCanReadSensorAsync(ApplicationUser user, PinnedSensor resource)
        {
            return Task.FromResult(resource.UserId.Equals(user.Id));
        }

        public async Task<bool> UserCanUpdateSensorAsync(ClaimsPrincipal principal, PinnedSensor resource)
        {
            var user = await userService.GetUserAsync(principal);

            if (user == null)
            {
                throw new ArgumentException("Could not find user for claims principal.", nameof(principal));
            }

            return await UserCanUpdateSensorAsync(user, resource);
        }

        public Task<bool> UserCanUpdateSensorAsync(ApplicationUser user, PinnedSensor resource)
        {
            return Task.FromResult(resource.UserId.Equals(user.Id));
        }

        public async Task<bool> UserCanDeleteSensorAsync(ClaimsPrincipal principal, PinnedSensor resource)
        {
            var user = await userService.GetUserAsync(principal);

            if (user == null)
            {
                throw new ArgumentException("Could not find user for claims principal.", nameof(principal));
            }

            return await UserCanDeleteSensorAsync(user, resource);
        }

        public Task<bool> UserCanDeleteSensorAsync(ApplicationUser user, PinnedSensor resource)
        {
            return Task.FromResult(resource.UserId.Equals(user.Id));
        }
    }
}
