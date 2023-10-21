namespace SkySaver.Domain.UserPurchases.Services;

using SkySaver.Domain.UserPurchases;
using SkySaver.Databases;
using SkySaver.Services;

public interface IUserPurchaseRepository : IGenericRepository<UserPurchase>
{
}

public sealed class UserPurchaseRepository : GenericRepository<UserPurchase>, IUserPurchaseRepository
{
    private readonly SkySaverDbContext _dbContext;

    public UserPurchaseRepository(SkySaverDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
