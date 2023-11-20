namespace API.Domain.Dto;

public class PinnedCityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public DateTime CreatedAt { get; set; }
    public double Longitude { get; set; }
}