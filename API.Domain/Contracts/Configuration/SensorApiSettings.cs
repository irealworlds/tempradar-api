namespace API.Domain.Contracts.Configuration;

public class SensorApiSettings
{
    public required string BaseUri { get; init; }
    public required string ApiKey { get; init; }
}