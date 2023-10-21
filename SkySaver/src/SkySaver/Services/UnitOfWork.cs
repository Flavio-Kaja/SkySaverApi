namespace SkySaver.Services;

using SkySaver.Databases;

public interface IUnitOfWork : ISkySaverScopedService
{
    Task<int> CommitChanges(CancellationToken cancellationToken = default);
}

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly SkySaverDbContext _dbContext;

    public UnitOfWork(SkySaverDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CommitChanges(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
