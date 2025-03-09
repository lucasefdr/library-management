using LibraryManagementSystem.Core.Enums;

namespace LibraryManagementSystem.Application.ViewModels.Book;

public class BookViewModel
{
    public BookViewModel(Guid id, string title, string author, string isbn, Status status, int publicationYear)
    {
        Id = id;
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
        Status = status.ToString();
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public string Status { get; private set; }
    public int PublicationYear { get; private set; }
}
