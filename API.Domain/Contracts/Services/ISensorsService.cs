using API.Domain.Dto;

namespace API.Domain.Contracts.Services;

public interface ISensorsService
{
    public Task<IEnumerable<SensorDto>> GetSensorsAsync(int? skip = null, int? limit = null);

    public Task<IEnumerable<SensorReadingDto>> GetSensorReadingsAsync(string resourceIdentifier, int? skip = null,
        int? limit = null);
}