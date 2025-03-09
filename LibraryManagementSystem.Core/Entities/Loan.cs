namespace LibraryManagementSystem.Core.Entities;

public sealed class Loan : BaseEntity
{
    private Loan(Guid userId, Guid bookId)
    {
        UserId = userId;
        BookId = bookId;
        CheckoutDate = DateTime.UtcNow;
    }

    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public DateTime CheckoutDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }  // Retorno pode ser nulo inicialmente

    #region Navigation Properties/Relacionamentos
    public User User { get; private set; } = null!;
    public Book Book { get; private set; } = null!;
    #endregion

    public static Loan LoanCheckout(User user, Book book)
    {
        book.BookLoan();
        return new Loan(user.Id, book.Id) { Book = book, User = user }; // Atribui as referências
    }

    public void LoanReturn()
    {
        if (Book is null)
        {
            throw new InvalidOperationException("Book reference is not loaded.");
        }

        ReturnDate = DateTime.UtcNow;
        Book.BookReturn();
    }
}
