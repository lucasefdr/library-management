using LibraryManagement.Application.Common;
using LibraryManagement.Application.DTOs.InputModels.Borrowing;
using LibraryManagement.Application.Repositories;
using LibraryManagement.Application.Services.Interfaces;
using LibraryManagement.Core.Common;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using LibraryManagementSystem.Application.ViewModels.Borrowing;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagement.Application.Services.Implementations;

public class BorrowingService(
    IBorrowingRepository borrowingRepository,
    IBookRepository bookRepository,
    IUserRepository userRepository)
    : IBorrowingService
{
    public async Task<PagedResult<BorrowingViewModel>> GetAllBorrowings(QueryParameters parameters)
    {
        var borrowings = await borrowingRepository.ReadAllAsync(parameters);

        var response = new PagedResult<BorrowingViewModel>()
        {
            CurrentPage = borrowings.CurrentPage,
            PageSize = borrowings.PageSize,
            TotalCount = borrowings.TotalCount,
            TotalPages = borrowings.TotalPages,
            Items =
            [
                ..borrowings.Items.Select(b =>
                    new BorrowingViewModel(b.Id, b.User.Name, b.Book.Title, b.CheckoutDate, b.DueDate, b.ReturnDate))
            ]
        };

        return response;
    }
    
    public async Task<Result<BorrowingViewModel>> GetBorrowing(int id)
    {
        var borrowing = await borrowingRepository.ReadAsync(id);

        if (borrowing == null)
            return Result.Failure<BorrowingViewModel>("Borrowing not found ", 404);

        var response = new BorrowingViewModel(borrowing.Id,
            borrowing.User.Name, borrowing.Book.Title, borrowing.CheckoutDate,
            borrowing.DueDate, borrowing.ReturnDate);

        return Result.Success(response);
    }

    public async Task<Result<int>> CreateBorrowing(CreateBorrowingInputModel model)
    {
        var result = await ValidateUserAndBook(model.UserId, model.BookId);
        if (result.IsFailure)
            return Result.Failure<int>(result.ErrorMessage, result.StatusCode);

        var borrowing = Borrowing.Checkout(result.Value.user, result.Value.book, model.DueDate);

        await borrowingRepository.CreateAsync(borrowing);
        return Result.Success(borrowing.Id);
    }

    public async Task<Result> ReturnBorrowing(int id)
    {
        var borrowing = await borrowingRepository.FindAsync(id);
        if (borrowing is null)
            return Result.Failure("Borrowing not found", 404);

        if (borrowing.ReturnDate.HasValue)
            return Result.Failure("Book is already returned", 400);

        var book = await bookRepository.FindAsync(borrowing.BookId);
        if (book is null)
            return Result.Failure("Book not found", 404);

        borrowing.Return(book);

        await borrowingRepository.UpdateAsync(borrowing);

        return Result.Success();
    }

    /// <summary>
    /// Valida se o usuário e o livro existem e se o livro está disponível para empréstimo.
    /// </summary>
    private async Task<Result<(User user, Book book)>> ValidateUserAndBook(int userId, int bookId)
    {
        var book = await bookRepository.FindAsync(bookId);
        if (book is null)
            return Result.Failure<(User user, Book book)>("Book not found", 404);

        if (book.Status == Status.Unavailable)
            return Result.Failure<(User user, Book book)>("Book is not available", 400);

        var user = await userRepository.FindAsync(userId);
        if (user is null)
            return Result.Failure<(User user, Book book)>("User not found", 404);

        return Result.Success((user, book));
    }
}