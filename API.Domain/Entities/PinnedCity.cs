namespace API.Domain.Entities;

public class PinnedCity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
}