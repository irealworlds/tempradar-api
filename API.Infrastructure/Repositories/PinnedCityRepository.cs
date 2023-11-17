using API.Domain.Entities;
using API.Domain.Repositories;
using API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;

public class PinnedCityRepository(AppDbContext dbContext) : AbstractRepository<PinnedCity, Guid>(dbContext), IPinnedCityRepository;