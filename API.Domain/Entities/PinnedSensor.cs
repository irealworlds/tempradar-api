using API.Domain.Contracts;

namespace API.Domain.Entities
{
    public class PinnedSensor : IEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string SensorId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; }
    }
}
