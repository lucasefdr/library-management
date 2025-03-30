using LibraryManagement.Application.Common;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagement.Application.Repositories;

public interface IUserRepository 
{
    Task<int> CreateAsync(User entity);
    Task<PagedResult<User>> ReadAllAsync(QueryParameters parameters);
    Task<User?> ReadAsync(int id);
    Task<User?> FindAsync(int id);
}
