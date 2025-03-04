using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Repositories;

namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {

    }
}
