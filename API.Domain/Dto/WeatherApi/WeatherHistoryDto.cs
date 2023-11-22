namespace API.Domain.Dto.WeatherApi
{
    public class WeatherHistoryDto
    {
        public LocationDto? Location { get; set; }
        public ForecastDto? Forecast { get; set; }
    }
}
