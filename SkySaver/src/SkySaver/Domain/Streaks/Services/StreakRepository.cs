namespace SkySaver.Domain.Streaks.Services;

using SkySaver.Domain.Streaks;
using SkySaver.Databases;
using SkySaver.Services;

public interface IStreakRepository : IGenericRepository<Streak>
{
}

public sealed class StreakRepository : GenericRepository<Streak>, IStreakRepository
{
    private readonly SkySaverDbContext _dbContext;

    public StreakRepository(SkySaverDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
