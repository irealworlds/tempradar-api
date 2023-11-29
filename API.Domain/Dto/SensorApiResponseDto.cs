using Newtonsoft.Json;

namespace API.Domain.Dto;

public class SensorApiResponseDto
{
    [JsonProperty("items")] public IEnumerable<SensorDto>? Items { get; set; }
}