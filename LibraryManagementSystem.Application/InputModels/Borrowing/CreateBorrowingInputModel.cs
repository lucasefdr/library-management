namespace LibraryManagementSystem.Application.InputModels.Borrowing;

public record CreateBorrowingInputModel
{
    public required Guid UserId { get; set; }
    public required Guid BookId { get; set; }
    public required DateTime DueDate { get; set; }
}
