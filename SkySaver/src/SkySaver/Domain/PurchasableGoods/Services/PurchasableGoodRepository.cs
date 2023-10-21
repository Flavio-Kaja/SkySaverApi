namespace SkySaver.Domain.PurchasableGoods.Services;

using SkySaver.Domain.PurchasableGoods;
using SkySaver.Databases;
using SkySaver.Services;

public interface IPurchasableGoodRepository : IGenericRepository<PurchasableGood>
{
}

public sealed class PurchasableGoodRepository : GenericRepository<PurchasableGood>, IPurchasableGoodRepository
{
    private readonly SkySaverDbContext _dbContext;

    public PurchasableGoodRepository(SkySaverDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
