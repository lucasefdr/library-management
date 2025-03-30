using LibraryManagement.Application.Common;
using LibraryManagement.Application.Repositories;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Extensions;
using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories;

public class BorrowingRepository(IRepository<Borrowing> repository) : IBorrowingRepository
{
    public async Task<int> CreateAsync(Borrowing entity)
    {
        await repository.CreateAsync(entity);
        return entity.Id;
    }

    public async Task<PagedResult<Borrowing>> ReadAllAsync(QueryParameters parameters)
    {
        string[] searchableProperties = [""];

        var query = repository.GetAll()
            .Include(b => b.User)
            .Include(b => b.Book)
            .AsNoTracking()
            .ApplyQueryParameters(parameters, searchableProperties);

        return await query.ToPagedResultAsync(parameters);
    }

    public async Task<Borrowing?> ReadAsync(int id)
    {
        return await repository.GetAll()
            .AsNoTracking()
            .Include(b => b.User)
            .Include(b => b.Book)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public Task<Borrowing?> FindAsync(int id)
    {
        return repository.FindAsync(id);
    }

    public async Task UpdateAsync(Borrowing entity)
    {
        await repository.UpdateAsync(entity);
    }
}