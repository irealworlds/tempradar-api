namespace API.Domain.Dto;

public class CreatePinnedCityDto
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}