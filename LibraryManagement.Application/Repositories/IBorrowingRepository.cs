using LibraryManagement.Application.Common;
using LibraryManagement.Core.Entities;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagement.Application.Repositories;

public interface IBorrowingRepository 
{
    Task<int> CreateAsync(Borrowing entity);
    Task<PagedResult<Borrowing>> ReadAllAsync(QueryParameters parameters);
    Task<Borrowing?> ReadAsync(int id);
    Task<Borrowing?> FindAsync(int id);
    Task UpdateAsync(Borrowing entity);

}
