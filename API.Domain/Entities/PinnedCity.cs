using API.Domain.Contracts;

namespace API.Domain.Entities;

public class PinnedCity : IEntity<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
}