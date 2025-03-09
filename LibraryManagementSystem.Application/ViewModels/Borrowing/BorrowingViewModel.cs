namespace LibraryManagementSystem.Application.ViewModels.Borrowing;

public record BorrowingViewModel
{
    public BorrowingViewModel(Guid id, string userName, string bookTitle, DateTime checkoutDate, DateTime dueDate, DateTime? returnDate)
    {
        Id = id;
        UserName = userName;
        BookTitle = bookTitle;
        CheckoutDate = checkoutDate;
        DueDate = dueDate;
        ReturnDate = returnDate;
    }

    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string BookTitle { get; init; }
    public DateTime CheckoutDate { get; init; }
    public DateTime DueDate { get; init; }
    public DateTime? ReturnDate { get; init; }  // Permite valores nulos
}
