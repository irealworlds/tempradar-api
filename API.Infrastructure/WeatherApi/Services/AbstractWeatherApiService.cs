using System.Web;
using API.Domain.Contracts.Configuration;
using Microsoft.Extensions.Options;

namespace API.Infrastructure.WeatherApi.Services;

public abstract class AbstractWeatherApiService(IOptions<WeatherApiSettings> options)
{
    private readonly WeatherApiSettings _weatherApiSettings = options.Value;

    protected string BuildEndpointUri(string endpoint, IDictionary<string, string> queryParams)
    {
        var formattedEndpoint = endpoint.Trim('/');
        var absoluteUriPath = Path.Combine(this._weatherApiSettings.BaseUri.TrimEnd('/'), formattedEndpoint);

        var builder = new UriBuilder(absoluteUriPath);

        var query = HttpUtility.ParseQueryString(builder.Query);

        // Add key to the query params
        query["key"] = this._weatherApiSettings.ApiKey;

        // Add other query parameters
        foreach (var param in queryParams) query[param.Key] = param.Value;

        builder.Query = query.ToString();

        return builder.ToString();
    }
}