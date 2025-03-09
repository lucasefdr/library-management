namespace LibraryManagementSystem.Application.InputModels.Book;

public record CreateBookInputModel
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required string ISBN { get; set; }
    public required int PublicationYear { get; set; }
}
