using FluentValidation;
using LibraryManagement.Application.DTOs.InputModels.Book;

namespace LibraryManagement.Application.Validators.Book;

public class CreateBookInputModelValidator : AbstractValidator<CreateBookInputModel>
{
    public CreateBookInputModelValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(1, 128).WithMessage("{PropertyName} must be between 1 and 128 characters.");

        RuleFor(b => b.Author)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(1, 128).WithMessage("{PropertyName} must be between 1 and 128 characters.");

        RuleFor(b => b.ISBN)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(_isbnValidator).WithMessage("{PropertyName} must have 10 or 13 characters.");
        
        RuleFor(b => b.PublicationYear)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(_publicationYearValidator).WithMessage("{PropertyName} should have a valid year.");
    }

    private readonly Func<string, bool> _isbnValidator = isbn => isbn.Length is 10 or 13;
    
    private readonly Func<int, bool> _publicationYearValidator = year => year is >= 0 and <= 9999;
}