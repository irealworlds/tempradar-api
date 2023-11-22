using Newtonsoft.Json;

namespace API.Domain.Dto.WeatherApi
{
    public class DailyConditionsDto
    {
        [JsonProperty("maxtemp_c")]
        public double MaximumTemperatureCelsius { get; set; }

        [JsonProperty("maxtemp_f")]
        public double MaximumTemperatureFahrenheit { get; set; }

        [JsonProperty("mintemp_c")]
        public double MinimumTemperatureCelsius { get; set; }

        [JsonProperty("mintemp_f")]
        public double MinimumTemperatureFahrenheit { get; set; }

        [JsonProperty("avgtemp_c")]
        public double AverageTemperatureCelsius { get; set; }

        [JsonProperty("avgtemp_f")]
        public double AverageTemperatureFahrenheit { get; set; }

        [JsonProperty("maxwind_mph")]
        public double MaximumWindMph { get; set; }

        [JsonProperty("maxwind_kph")]
        public double MaximumWindKph { get; set; }

        [JsonProperty("totalprecip_mm")]
        public double TotalPrecipitationsMm { get; set; }

        [JsonProperty("totalprecip_in")]
        public double TotalPrecipitationsIn { get; set; }

        [JsonProperty("totalsnow_cm")]
        public double TotalSnowCm { get; set; }

        [JsonProperty("avgvis_km")]
        public double AverageVisibilityKm { get; set; }

        [JsonProperty("avgvis_miles")]
        public double AverageVisibilityMiles { get; set; }

        [JsonProperty("avghumidity")]
        public double AverageHumidity { get; set; }

        [JsonProperty("daily_will_it_rain")]
        public int WillItRain { get; set; }

        [JsonProperty("daily_chance_of_rain")]
        public int ChanceOfRain { get; set; }

        [JsonProperty("daily_will_it_snow")]
        public int WillItSnow { get; set; }

        [JsonProperty("daily_chance_of_snow")]
        public int ChanceOfSnow { get; set; }

        [JsonProperty("condition")]
        public WeatherConditionDto? Condition { get; set; }

        [JsonProperty("uv")]
        public double UltravioletIndex { get; set; }

        [JsonProperty("air_quality")]
        public AirQualityDto? AirQuality { get; set; }
    }
}
