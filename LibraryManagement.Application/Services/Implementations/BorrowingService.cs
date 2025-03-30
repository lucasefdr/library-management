using LibraryManagement.Application.DTOs.InputModels.Borrowing;
using LibraryManagement.Core.Enums;
using LibraryManagementSystem.Application.Services.Interfaces;
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

    public BorrowingService(IBorrowingRepository borrowingRepository, IBookRepository bookRepository, IUserRepository userRepository)
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

    public async Task<BorrowingViewModel?> GetBorrowing(int id)
    {
        var borrowing = await _borrowingRepository.GetWithDetailsAsync(id);
        return borrowing is null
            ? null
            : new BorrowingViewModel(borrowing.Id, borrowing.User.Name, borrowing.Book.Title, borrowing.CheckoutDate, borrowing.DueDate, borrowing.ReturnDate);
    }

    public async Task<int> CreateBorrowing(CreateBorrowingInputModel model)
    {
        var (user, book) = await ValidateUserAndBook(model.UserId, model.BookId);

        var borrowing = Borrowing.Checkout(user, book, model.DueDate);

        await _borrowingRepository.CreateAsync(borrowing);
        return borrowing.Id;
    }

    public async Task ReturnBorrowing(int id)
    {
        var borrowing = await _borrowingRepository.FindAsync(id);
        if (borrowing is null) return;

        var book = await _bookRepository.FindAsync(borrowing.BookId);

        borrowing.Return(book!);

        await _borrowingRepository.UpdateAsync(borrowing);
    }

    /// <summary>
    /// Valida se o usuário e o livro existem e se o livro está disponível para empréstimo.
    /// </summary>
    private async Task<(User user, Book book)> ValidateUserAndBook(int userId, int bookId)
    {
        var book = await _bookRepository.FindAsync(bookId) ?? throw new ArgumentNullException(nameof(bookId), "Book not exists.");

        if (book.Status == Status.Unavailable)
            throw new InvalidOperationException("The book is currently unavailable.");

        var user = await _userRepository.FindAsync(userId) ?? throw new ArgumentNullException(nameof(userId), "User not exists.");

        return (user, book);
    }
}
