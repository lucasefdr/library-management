using LibraryManagementSystem.Application.InputModels.Loan;
using LibraryManagementSystem.Application.ViewModels.Loan;

namespace LibraryManagementSystem.Application.Services.Interfaces;

public interface ILoanService
{
    Task<IEnumerable<LoanViewModel>> GetAllLoans();
    Task<LoanViewModel?> GetLoan(Guid id);
    Task<Guid> CreateLoan(CreateLoanInputModel model);
    Task ReturnLoan(Guid id);
}
