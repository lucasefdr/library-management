using LibraryManagementSystem.Core.VOs;

namespace LibraryManagementSystem.Core.Entities;

public sealed class Book : BaseEntity
{
    public Book(string title, string author, ISBN isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;

        LoanList = [];
    }

    public string Title { get; private set; }
    public string Author { get; private set; }
    public ISBN ISBN { get; private set; }
    public int PublicationYear { get; private set; }
    public List<Loan> LoanList { get; private set; }
}
