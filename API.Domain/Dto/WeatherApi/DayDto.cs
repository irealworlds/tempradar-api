using Newtonsoft.Json;

namespace API.Domain.Dto.WeatherApi
{
    public class DayDto
    {
        [JsonProperty("maxtemp_c")]
        public double MaxTempC { get; set; }

        [JsonProperty("maxtemp_f")]
        public double MaxTempF { get; set; }

        [JsonProperty("mintemp_c")]
        public double MinTempC { get; set; }

        [JsonProperty("mintemp_f")]
        public double MinTempF { get; set; }

        [JsonProperty("avgtemp_c")]
        public double AvgTempC { get; set; }

        [JsonProperty("avgtemp_f")]
        public double AvgTempF { get; set; }

        [JsonProperty("maxwind_mph")]
        public double MaxWindMph { get; set; }

        [JsonProperty("maxwind_kph")]
        public double MaxWindKph { get; set; }

        [JsonProperty("totalprecip_mm")]
        public double TotalPrecipMm { get; set; }

        [JsonProperty("totalprecip_in")]
        public double TotalPrecipIn { get; set; }

        [JsonProperty("totalsnow_cm")]
        public double TotalSnowCm { get; set; }

        [JsonProperty("avgvis_km")]
        public double AvgVisibilityKm { get; set; }

        [JsonProperty("avgvis_miles")]
        public double AvgVisibilityMiles { get; set; }

        [JsonProperty("avghumidity")]
        public double AvgHumidity { get; set; }

        [JsonProperty("daily_will_it_rain")]
        public int WillItRain { get; set; }

        [JsonProperty("daily_chance_of_rain")]
        public int ChanceOfRain { get; set; }

        [JsonProperty("daily_will_it_snow")]
        public int WillItSnow { get; set; }

        [JsonProperty("daily_chance_of_snow")]
        public int ChanceOfSnow { get; set; }

        [JsonProperty("condition")]
        public ConditionDto? Condition { get; set; }

        [JsonProperty("uv")]
        public double UV { get; set; }

        [JsonProperty("air_quality")]
        public AirQualityDto? AirQuality { get; set; }
    }
}
