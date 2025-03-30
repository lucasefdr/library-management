using LibraryManagement.Application.Common;
using LibraryManagement.Application.DTOs.InputModels.Borrowing;
using LibraryManagement.Core.Common;
using LibraryManagementSystem.Application.ViewModels.Borrowing;

namespace LibraryManagement.Application.Services.Interfaces;

public interface IBorrowingService
{
    Task<PagedResult<BorrowingViewModel>> GetAllBorrowings(QueryParameters parameters);
    Task<Result<BorrowingViewModel>> GetBorrowing(int id);
    Task<Result<int>> CreateBorrowing(CreateBorrowingInputModel model);
    Task<Result> ReturnBorrowing(int id);
}
