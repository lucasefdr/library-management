using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.VOs;

namespace LibraryManagementSystem.Core.Entities;

public sealed class Book : BaseEntity
{
    private Book() { }

    public Book(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = ISBN.Create(isbn);
        PublicationYear = publicationYear;
        Status = Status.Available;

        LoanList = [];
    }

    public string Title { get; private set; }
    public string Author { get; private set; }
    public ISBN ISBN { get; private set; }
    public int PublicationYear { get; private set; }
    public Status Status { get; private set; }

    #region Navigation Properties/Relacionamentos
    public List<Loan> LoanList { get; private set; }
    #endregion

    public void BookLoan()
    {
        Status = Status.Unavailable;
    }

    public void BookReturn()
    {
        Status = Status.Available;
    }

}
