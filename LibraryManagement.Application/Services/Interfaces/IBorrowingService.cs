using LibraryManagement.Application.DTOs.InputModels.Borrowing;
using LibraryManagement.Core.Common;
using LibraryManagementSystem.Application.ViewModels.Borrowing;

namespace LibraryManagement.Application.Services.Interfaces;

public interface IBorrowingService
{
    Task<IEnumerable<BorrowingViewModel>> GetAllBorrowings();
    Task<Result<BorrowingViewModel>> GetBorrowing(int id);
    Task<Result<int>> CreateBorrowing(CreateBorrowingInputModel model);
    Task<Result> ReturnBorrowing(int id);
}
