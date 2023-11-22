namespace API.Domain.Dto;

public class PinnedCityWeatherDetailsDto
{
    public double Temperature { get; set; }
    public double FeelsLikeTemperature { get; set; }
    public double WindSpeed { get; set; }
    public double AtmosphericPressure { get; set; }
    public double UltravioletIndex { get; set; }
    public double CarbonMonoxide { get; set; }
    public double NitrogenDioxide { get; set; }
    public double Ozone { get; set; }
    public double SulphurDioxide { get; set; }
    public double Pm25 { get; set; }
    public double Pm10 { get; set; }
    public int UsEpaIndex { get; set; }
    public int GbDefraIndex { get; set; }
}