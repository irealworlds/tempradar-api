using API.Domain.Entities;
using API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;

public class PinnedCityRepository(DbContext dbContext) : AbstractRepository<PinnedCity, Guid>(dbContext), IPinnedCityRepository;