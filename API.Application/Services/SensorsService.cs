using System.Text.Json;
using API.Domain.Contracts.Configuration;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using Microsoft.Extensions.Options;

namespace API.Application.Services;

public class SensorsService : ISensorsService
{
    private readonly SensorApiSettings _sensorsApiSettings;
    private readonly string baseUri;
    private readonly HttpClient client;

    public SensorsService(HttpClient client, IOptions<SensorApiSettings> options)
    {
        this.client = client;
        this._sensorsApiSettings = options.Value;
        this.baseUri = "http://sensor.irealworlds.com";

        client.DefaultRequestHeaders.Add("x-api-key", this._sensorsApiSettings.ApiKey);
    }

    public async Task<IEnumerable<SensorDto>> GetSensorsAsync(int? skip = null, int? limit = null)
    {
        var requestUri = this.BuildEndpointUri("/sensors", skip, limit);

        var response = await this.client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(
                $"Failed to retrieve sensors data. Status code: {response.StatusCode}"
            );

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var sensorApiResponse = JsonSerializer.Deserialize<SensorApiResponseDto>(content, options);
        if (sensorApiResponse == null) throw new Exception("Could not parse data returned from SensorsApi.");

        var sensors = sensorApiResponse.Items;
        if (sensors == null) throw new Exception("Could not parse data returned from SensorsApi.");

        return sensors;
    }

    public async Task<IEnumerable<SensorReadingDto>> GetSensorReadingsAsync(string resourceIdentifier, int? skip = null,
        int? limit = null)
    {
        var requestUri = this.BuildEndpointUri($"/sensors/{resourceIdentifier}/readings", skip, limit);

        var response = await this.client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(
                $"Failed to retrieve sensors data. Status code: {response.StatusCode}"
            );

        var content = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var sensorApiResponse = JsonSerializer.Deserialize<SensorReadingApiResponseDto>(content, options);
        if (sensorApiResponse == null) throw new Exception("Could not parse data returned from SensorsApi.");

        var sensorReadings = sensorApiResponse.Items;
        if (sensorReadings == null) throw new Exception("Could not parse data returned from SensorsApi.");

        return sensorReadings;
    }

    protected string BuildEndpointUri(string endpoint, int? skip = null, int? limit = null)
    {
        var uriBuilder = new UriBuilder(new Uri(new Uri(this.baseUri), endpoint));
        var queryParameters = new List<string>();

        if (skip != null) queryParameters.Add($"skip={skip}");
        if (limit != null) queryParameters.Add($"limit={limit}");
        if (queryParameters.Count > 0) uriBuilder.Query = string.Join("&", queryParameters);

        return uriBuilder.Uri.ToString();
    }
}