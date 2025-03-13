using LibraryManagementSystem.Application.InputModels.Borrowing;
using LibraryManagementSystem.Application.ViewModels.Borrowing;

namespace LibraryManagementSystem.Application.Services.Interfaces;

public interface IBorrowingService
{
    Task<IEnumerable<BorrowingViewModel>> GetAllBorrowings();
    Task<BorrowingViewModel?> GetBorrowing(Guid id);
    Task<Guid> CreateBorrowing(CreateBorrowingInputModel model);
    Task ReturnBorrowing(Guid id);
}
