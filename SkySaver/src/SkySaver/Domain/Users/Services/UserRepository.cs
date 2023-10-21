namespace SkySaver.Domain.Users.Services;

using SkySaver.Domain.Users;
using SkySaver.Databases;
using SkySaver.Services;

public interface IUserRepository : IGenericRepository<User>
{
}

public sealed class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly SkySaverDbContext _dbContext;

    public UserRepository(SkySaverDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
