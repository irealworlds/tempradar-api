namespace API.Domain.Dto;

public class PinnedSensorDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? SensorId { get; set; }
    public DateTime CreatedAt { get; set; }
}