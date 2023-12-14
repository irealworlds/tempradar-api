using System.Text.Json.Serialization;

namespace API.Domain.Dto.SensorApi;

public class SensorApiPaginatedResponseDto<TEntity> where TEntity : class
{
    [JsonPropertyName("items")]
    public required IEnumerable<TEntity> Items { get; set; }

    [JsonPropertyName("_metadata")]
    public required SensorApiPaginationMetadata Metadata { get; set; }
}