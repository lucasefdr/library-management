using LibraryManagement.Application.DTOs.InputModels.Borrowing;
using LibraryManagement.Application.Services.Interfaces;
using LibraryManagement.Core.Common;
using LibraryManagement.Core.Enums;
using LibraryManagementSystem.Application.ViewModels.Borrowing;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Services.Implementations;

public class BorrowingService : IBorrowingService
{
    private readonly IBorrowingRepository _borrowingRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;

    public BorrowingService(IBorrowingRepository borrowingRepository, IBookRepository bookRepository,
        IUserRepository userRepository)
    {
        _borrowingRepository = borrowingRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<BorrowingViewModel>> GetAllBorrowings()
    {
        return await _borrowingRepository.GetAll()
            .AsNoTracking()
            .Include(borrowing => borrowing.User)
            .Include(borrowing => borrowing.Book)
            .Select(borrowing => new BorrowingViewModel(
                borrowing.Id,
                borrowing.User.Name,
                borrowing.Book.Title,
                borrowing.CheckoutDate,
                borrowing.DueDate,
                borrowing.ReturnDate))
            .ToListAsync();
    }

    public async Task<Result<BorrowingViewModel>> GetBorrowing(int id)
    {
        var borrowing = await _borrowingRepository.GetWithDetailsAsync(id);

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

        await _borrowingRepository.CreateAsync(borrowing);
        return Result.Success(borrowing.Id);
    }

    public async Task<Result> ReturnBorrowing(int id)
    {
        var borrowing = await _borrowingRepository.FindAsync(id);
        if (borrowing is null)
            return Result.Failure("Borrowing not found", 404);
        
        if (borrowing.ReturnDate.HasValue)
            return Result.Failure("Book is already returned", 400);

        var book = await _bookRepository.FindAsync(borrowing.BookId);
        if (book is null)
            return Result.Failure("Book not found", 404);

        borrowing.Return(book);

        await _borrowingRepository.UpdateAsync(borrowing);
        
        return Result.Success();
    }

    /// <summary>
    /// Valida se o usuário e o livro existem e se o livro está disponível para empréstimo.
    /// </summary>
    private async Task<Result<(User user, Book book)>> ValidateUserAndBook(int userId, int bookId)
    {
        var book = await _bookRepository.FindAsync(bookId);
        if (book is null)
            return Result.Failure<(User user, Book book)>("Book not found", 404);

        if (book.Status == Status.Unavailable)
            return Result.Failure<(User user, Book book)>("Book is not available", 400);

        var user = await _userRepository.FindAsync(userId);
        if (user is null)
            return Result.Failure<(User user, Book book)>("User not found", 404);

        return Result.Success((user, book));
    }
}