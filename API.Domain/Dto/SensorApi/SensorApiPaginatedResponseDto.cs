using Newtonsoft.Json;

namespace API.Domain.Dto.SensorApi;

public class SensorApiPaginatedResponseDto<TEntity> where TEntity : class
{
    [JsonProperty("items")]
    public required IEnumerable<TEntity> Items { get; set; }

    [JsonProperty("_meta")]
    public required SensorApiPaginationMetadata Metadata { get; set; }
}