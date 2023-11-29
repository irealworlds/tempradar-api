using System.Text.Json;
using API.Domain.Contracts.Configuration;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using API.Domain.Dto.SensorApi;
using Microsoft.Extensions.Options;

namespace API.Infrastructure.SensorApi.Services;

public class SensorsService : ISensorsService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    private readonly string _baseUri;
    private readonly HttpClient _client;

    public SensorsService(HttpClient client, IOptions<SensorApiSettings> options)
    {
        this._baseUri = options.Value.BaseUri;
        this._client = client;
        this._client.DefaultRequestHeaders.Add("x-api-key", options.Value.ApiKey);
    }

    public async Task<PaginatedResultDto<SensorDto>> GetSensorsAsync(int? skip = null, int? limit = null)
    {
        var requestUri = this.BuildEndpointUri("/sensors", skip, limit);

        var response = await this._client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Failed to retrieve sensors data. Status code: {response.StatusCode}"
            );
        }

        var content = await response.Content.ReadAsStringAsync();

        var sensorApiResponse = JsonSerializer.Deserialize<SensorApiPaginatedResponseDto<SensorDto>>(content, SensorsService.JsonOptions);
        if (sensorApiResponse == null) throw new Exception("Could not parse data returned from SensorsApi.");

        return new PaginatedResultDto<SensorDto>
        {
            Items = sensorApiResponse.Items ?? throw new InvalidOperationException(),
            Total = sensorApiResponse.Metadata.Total,
        };
    }

    public async Task<PaginatedResultDto<SensorReadingDto>> GetSensorReadingsAsync(string resourceIdentifier, int? skip = null,
        int? limit = null)
    {
        var requestUri = this.BuildEndpointUri($"/sensors/{resourceIdentifier}/readings", skip, limit);

        var response = await this._client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(
                $"Failed to retrieve sensors data. Status code: {response.StatusCode}"
            );

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var sensorApiResponse = JsonSerializer.Deserialize<SensorApiPaginatedResponseDto<SensorReadingDto>>(content, options);
        if (sensorApiResponse == null) throw new Exception("Could not parse data returned from SensorsApi.");

        return new PaginatedResultDto<SensorReadingDto>
        {
            Items = sensorApiResponse.Items ?? throw new InvalidOperationException(),
            Total = sensorApiResponse.Metadata.Total,
        };
    }

    private string BuildEndpointUri(string endpoint, int? skip = null, int? limit = null)
    {
        var uriBuilder = new UriBuilder(new Uri(new Uri(this._baseUri), endpoint));
        var queryParameters = new List<string>();

        if (skip != null) queryParameters.Add($"skip={skip}");
        if (limit != null) queryParameters.Add($"limit={limit}");
        if (queryParameters.Count > 0) uriBuilder.Query = string.Join("&", queryParameters);

        return uriBuilder.Uri.ToString();
    }
}