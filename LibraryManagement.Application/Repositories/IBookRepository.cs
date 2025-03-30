using LibraryManagement.Application.Common;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagement.Application.Repositories;

public interface IBookRepository 
{
    Task<int> CreateAsync(Book entity);
    Task<PagedResult<Book>> ReadAllAsync(QueryParameters parameters);
    Task<Book?> ReadAsync(int id);
    Task<Book?> FindAsync(int id);
}
