using LibraryManagement.Application.DTOs.InputModels.Borrowing;
using LibraryManagementSystem.Application.ViewModels.Borrowing;

namespace LibraryManagementSystem.Application.Services.Interfaces;

public interface IBorrowingService
{
    Task<IEnumerable<BorrowingViewModel>> GetAllBorrowings();
    Task<BorrowingViewModel?> GetBorrowing(int id);
    Task<int> CreateBorrowing(CreateBorrowingInputModel model);
    Task ReturnBorrowing(int id);
}
