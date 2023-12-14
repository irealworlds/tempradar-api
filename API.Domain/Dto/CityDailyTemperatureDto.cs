namespace API.Domain.Dto;

public class CityDailyTemperatureDto
{
    public DateTime Date { get; set; }
    public double MaximumTemperature { get; set; }
    public double MinimumTemperature { get; set; }
    public double AverageTemperature { get; set; }
}