using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces;

public interface IBorrowingRepository : IRepository<Borrowing>
{
    Task<Borrowing?> GetWithDetailsAsync(int id);
}
