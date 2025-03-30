namespace LibraryManagement.Application.DTOs.InputModels.Borrowing;

public record CreateBorrowingInputModel(
    int UserId,
    int BookId,
    DateOnly DueDate);