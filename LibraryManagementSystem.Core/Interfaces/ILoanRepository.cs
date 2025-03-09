using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces;

public interface ILoanRepository : IRepository<Loan>
{
    Task<Loan?> GetLoanWithDetailsAsync(Guid id);
}
