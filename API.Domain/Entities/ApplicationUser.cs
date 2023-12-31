using Microsoft.AspNetCore.Identity;

namespace API.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public virtual IEnumerable<PinnedCity> PinnedCities { get; }
    public virtual IEnumerable<PinnedSensor> PinnedSensors { get; }
}