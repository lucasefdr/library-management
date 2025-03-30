using LibraryManagement.Application.Common;
using LibraryManagement.Application.Repositories;
using LibraryManagement.Infrastructure.Extensions;
using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories;

public class BookRepository(IRepository<Book> repository) : IBookRepository
{
    public async Task<int> CreateAsync(Book entity)
    {
        await repository.CreateAsync(entity);
        return entity.Id;
    }

    public async Task<PagedResult<Book>> ReadAllAsync(QueryParameters parameters)
    {
        string[] searchableProperties = ["Title"];

        var query = repository.GetAll()
            .AsNoTracking()
            .ApplyQueryParameters(parameters, searchableProperties);

        return await query.ToPagedResultAsync(parameters);
    }

    public async Task<Book?> ReadAsync(int id)
    {
        return await repository.GetAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book?> FindAsync(int id)
    {
        return await repository.FindAsync(id);
    }
}