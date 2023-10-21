namespace SkySaver.Domain.ScavengerHunts.Services;

using SkySaver.Domain.ScavengerHunts;
using SkySaver.Databases;
using SkySaver.Services;

public interface IScavengerHuntRepository : IGenericRepository<ScavengerHunt>
{
}

public sealed class ScavengerHuntRepository : GenericRepository<ScavengerHunt>, IScavengerHuntRepository
{
    private readonly SkySaverDbContext _dbContext;

    public ScavengerHuntRepository(SkySaverDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
