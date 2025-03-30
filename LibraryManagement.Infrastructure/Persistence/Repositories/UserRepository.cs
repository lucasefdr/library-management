using LibraryManagement.Application.Common;
using LibraryManagement.Application.Repositories;
using LibraryManagement.Infrastructure.Extensions;
using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories;

public class UserRepository(IRepository<User> repository) : IUserRepository
{
    public async Task<int> CreateAsync(User entity)
    {
        await repository.CreateAsync(entity);
        return entity.Id;
    }

    public async Task<PagedResult<User>> ReadAllAsync(QueryParameters parameters)
    {
        string[] searchableProperties = ["Title"];

        var query = repository.GetAll()
            .AsNoTracking()
            .ApplyQueryParameters(parameters, searchableProperties);

        return await query.ToPagedResultAsync(parameters);
    }

    public async Task<User?> ReadAsync(int id)
    {
        return await repository.GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<User?> FindAsync(int id)
    {
        return await repository.FindAsync(id);
    }
}