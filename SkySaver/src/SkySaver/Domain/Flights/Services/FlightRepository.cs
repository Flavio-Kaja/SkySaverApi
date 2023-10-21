namespace SkySaver.Domain.Flights.Services;

using SkySaver.Domain.Flights;
using SkySaver.Databases;
using SkySaver.Services;

public interface IFlightRepository : IGenericRepository<Flight>
{
}

public sealed class FlightRepository : GenericRepository<Flight>, IFlightRepository
{
    private readonly SkySaverDbContext _dbContext;

    public FlightRepository(SkySaverDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
