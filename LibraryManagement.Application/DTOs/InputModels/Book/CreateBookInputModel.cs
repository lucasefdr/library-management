namespace LibraryManagement.Application.DTOs.InputModels.Book;

public record CreateBookInputModel(
    string Title,
    string Author,
    string ISBN,
    int PublicationYear);