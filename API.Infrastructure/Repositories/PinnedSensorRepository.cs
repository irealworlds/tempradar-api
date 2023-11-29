using API.Domain.Entities;
using API.Domain.Repositories;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class PinnedSensorRepository(AppDbContext dbContext) : AbstractRepository<PinnedSensor, Guid>(dbContext), IPinnedSensorRepository;
}
