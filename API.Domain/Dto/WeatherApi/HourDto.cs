﻿using Newtonsoft.Json;

namespace API.Domain.Dto.WeatherApi;

public class HourDto
{
    [JsonProperty("time_epoch")] public int TimeEpoch { get; set; }

    [JsonProperty("time")] public string? Time { get; set; }

    [JsonProperty("temp_c")] public double TemperatureCelsius { get; set; }

    [JsonProperty("temp_f")] public double TemperatureFahrenheit { get; set; }

    [JsonProperty("is_day")] public int IsDay { get; set; }

    [JsonProperty("condition")] public WeatherConditionDto? Condition { get; set; }

    [JsonProperty("wind_mph")] public double WindMph { get; set; }

    [JsonProperty("wind_kph")] public double WindKph { get; set; }

    [JsonProperty("wind_degree")] public int WindDegree { get; set; }

    [JsonProperty("wind_dir")] public string? WindDirection { get; set; }

    [JsonProperty("pressure_mb")] public double PressureMb { get; set; }

    [JsonProperty("pressure_in")] public double PressureIn { get; set; }

    [JsonProperty("precip_mm")] public double PrecipitationsMm { get; set; }

    [JsonProperty("precip_in")] public double PrecipitationsIn { get; set; }

    [JsonProperty("humidity")] public int Humidity { get; set; }

    [JsonProperty("cloud")] public int Cloud { get; set; }

    [JsonProperty("feelslike_c")] public double FeelsLikeCelsius { get; set; }

    [JsonProperty("feelslike_f")] public double FeelsLikeFahrenheit { get; set; }

    [JsonProperty("windchill_c")] public double WindChillCelsius { get; set; }

    [JsonProperty("windchill_f")] public double WindChillFahrenheit { get; set; }

    [JsonProperty("heatindex_c")] public double HeatIndexCelsius { get; set; }

    [JsonProperty("heatindex_f")] public double HeatIndexFahrenheit { get; set; }

    [JsonProperty("dewpoint_c")] public double DewPointCelsius { get; set; }

    [JsonProperty("dewpoint_f")] public double DewPointFahrenheit { get; set; }

    [JsonProperty("will_it_rain")] public int WillItRain { get; set; }

    [JsonProperty("chance_of_rain")] public int ChanceOfRain { get; set; }

    [JsonProperty("will_it_snow")] public int WillItSnow { get; set; }

    [JsonProperty("chance_of_snow")] public int ChanceOfSnow { get; set; }

    [JsonProperty("vis_km")] public double VisibilityKm { get; set; }

    [JsonProperty("vis_miles")] public double VisibilityMiles { get; set; }

    [JsonProperty("gust_mph")] public double GustMph { get; set; }

    [JsonProperty("gust_kph")] public double GustKph { get; set; }

    [JsonProperty("uv")] public double UltravioletIndex { get; set; }

    [JsonProperty("air_quality")] public AirQualityDto? AirQuality { get; set; }
}