using LibraryManagement.Core.Common;

namespace LibraryManagementSystem.Core.Entities;

public sealed class Borrowing : BaseEntity
{
    private Borrowing(int userId, int bookId)
    {
        UserId = userId;
        BookId = bookId;
        CheckoutDate = DateTime.UtcNow;
    }

    public int UserId { get; private set; }
    public int BookId { get; private set; }
    public DateTime CheckoutDate { get; private set; } // Data de retirada
    public DateTime DueDate { get; private set; } // Data prevista para devolução
    public DateTime? ReturnDate { get; private set; } // Data devolução

    #region Navigation Properties/Relacionamentos
    public User User { get; private set; } = null!;
    public Book Book { get; private set; } = null!;
    #endregion

    public static Borrowing Checkout(User user, Book book, DateOnly dueDate)
    {
        var dueDateTime = dueDate.ToDateTime(TimeOnly.MinValue);
        book.MarkAsBorrowed();
        return new Borrowing(user.Id, book.Id) { Book = book, User = user, DueDate = dueDateTime }; // Atribui as referências
    }

    public void Return(Book book)
    {
        ReturnDate = DateTime.UtcNow;
        Book.MarkAsAvailable();
    }
}
