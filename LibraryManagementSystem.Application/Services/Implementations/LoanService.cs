using LibraryManagementSystem.Application.InputModels.Loan;
using LibraryManagementSystem.Application.Services.Interfaces;
using LibraryManagementSystem.Application.ViewModels.Loan;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Services.Implementations;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;

    public LoanService(ILoanRepository loanRepository, IBookRepository bookRepository, IUserRepository userRepository)
    {
        _loanRepository = loanRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<LoanViewModel>> GetAllLoans()
    {
        return await _loanRepository.GetAll()
            .AsNoTracking()
            .Include(loan => loan.User)
            .Include(loan => loan.Book)
            .Select(loan => new LoanViewModel(
                loan.Id,
                loan.User.Name,
                loan.Book.Title,
                loan.CheckoutDate,
                loan.ReturnDate))
            .ToListAsync();
    }

    public async Task<LoanViewModel?> GetLoan(Guid id)
    {
        var loan = await _loanRepository.GetLoanWithDetailsAsync(id);
        return loan is null
            ? null
            : new LoanViewModel(loan.Id, loan.User.Name, loan.Book.Title, loan.CheckoutDate, loan.ReturnDate);
    }

    public async Task<Guid> CreateLoan(CreateLoanInputModel model)
    {
        var (user, book) = await ValidateUserAndBook(model.UserId, model.BookId);

        var loan = Loan.LoanCheckout(user, book);

        await _loanRepository.CreateAsync(loan);
        return loan.Id;
    }

    public async Task ReturnLoan(Guid id)
    {
        var loan = await _loanRepository.FindAsync(id);
        if (loan is null) return;

        loan.LoanReturn();

        await _loanRepository.UpdateAsync(loan);
    }

    /// <summary>
    /// Valida se o usuário e o livro existem e se o livro está disponível para empréstimo.
    /// </summary>
    private async Task<(User user, Book book)> ValidateUserAndBook(Guid userId, Guid bookId)
    {
        var book = await _bookRepository.FindAsync(bookId) ?? throw new ArgumentNullException(nameof(bookId), "Book not exists.");

        if (book.Status == Status.Unavailable)
            throw new InvalidOperationException("The book is currently unavailable.");

        var user = await _userRepository.FindAsync(userId) ?? throw new ArgumentNullException(nameof(userId), "User not exists.");

        return (user, book);
    }
}
