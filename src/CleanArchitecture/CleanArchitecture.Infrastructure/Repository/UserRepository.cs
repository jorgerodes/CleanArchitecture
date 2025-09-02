using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Infrastructure.Repository;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}

