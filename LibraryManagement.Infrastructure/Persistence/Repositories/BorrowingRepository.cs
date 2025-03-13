using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories;

public class BorrowingRepository : Repository<Borrowing>, IBorrowingRepository
{
    public BorrowingRepository(AppDbContext context) : base(context)
    {

    }

    public async Task<Borrowing?> GetWithDetailsAsync(Guid id)
    {
        var loan = await _context.Loans.AsNoTracking()
                                       .Include(l => l.User)
                                       .Include(l => l.Book)
                                       .FirstOrDefaultAsync(l => l.Id == id);
        return loan;
    }
}
