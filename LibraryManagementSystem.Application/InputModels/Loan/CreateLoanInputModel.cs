namespace LibraryManagementSystem.Application.InputModels.Loan;

public record CreateLoanInputModel
{
    public required Guid UserId { get; set; }
    public required Guid BookId { get; set; }
}
