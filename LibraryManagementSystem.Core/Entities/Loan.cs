namespace LibraryManagementSystem.Core.Entities;

public sealed class Loan : BaseEntity
{
    public Loan(int userId, int bookId)
    {
        UserId = userId;
        BookId = bookId;
        LoanDate = DateTime.UtcNow;
    }

    public int UserId { get; private set; }
    public User User { get; private set; }
    public int BookId { get; private set; }
    public Book Book { get; private set; }
    public DateTime LoanDate { get; private set; }
}
