using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories;

public class LoanRepository : Repository<Loan>, ILoanRepository
{
    public LoanRepository(AppDbContext context) : base(context)
    {

    }

    public async Task<Loan?> GetLoanWithDetailsAsync(Guid id)
    {
        var loan = await _context.Loans.AsNoTracking()
                                       .Include(l => l.User)
                                       .Include(l => l.Book)
                                       .FirstOrDefaultAsync(l => l.Id == id);
        return loan;
    }
}
