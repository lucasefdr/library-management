using FluentValidation;
using LibraryManagement.Application.DTOs.InputModels.Borrowing;

namespace LibraryManagement.Application.Validators.Borrowing;

public class CreateBorrowingInputModelValidator : AbstractValidator<CreateBorrowingInputModel>
{
    public CreateBorrowingInputModelValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(_dueDateInFuture).WithMessage("{PropertyName} must be in the future.");
    }

    private readonly Func<DateOnly, bool> _dueDateInFuture = dueDate => dueDate >= DateOnly.FromDateTime(DateTime.Today);
}