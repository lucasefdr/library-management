using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Repositories;

namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories;

public class LoanRepository : Repository<Loan>, ILoanRepository
{
    public LoanRepository(AppDbContext context) : base(context)
    {

    }
}
