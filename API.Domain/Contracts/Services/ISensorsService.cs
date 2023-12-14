using API.Domain.Dto;
using API.Domain.Dto.SensorApi;

namespace API.Domain.Contracts.Services;

public interface ISensorsService
{
    public Task<PaginatedResultDto<SensorDto>> GetSensorsAsync(int? skip = null, int? limit = null);

    public Task<PaginatedResultDto<SensorReadingDto>> GetSensorReadingsAsync(
        string resourceIdentifier,
        int? skip = null,
        int? limit = null
    );
}