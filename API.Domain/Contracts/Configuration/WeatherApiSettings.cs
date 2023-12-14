namespace API.Domain.Contracts.Configuration;

public class WeatherApiSettings
{
    public required string BaseUri { get; init; }
    public required string? ApiKey { get; init; }
}