namespace LibraryManagementSystem.Application.ViewModels.Loan;

public record LoanViewModel
{
    public LoanViewModel(Guid id, string userName, string bookTitle, DateTime checkoutDate, DateTime? returnDate)
    {
        Id = id;
        UserName = userName;
        BookTitle = bookTitle;
        CheckoutDate = checkoutDate;
        ReturnDate = returnDate;
    }

    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string BookTitle { get; init; }
    public DateTime CheckoutDate { get; init; }
    public DateTime? ReturnDate { get; init; }  // Permite valores nulos
}
