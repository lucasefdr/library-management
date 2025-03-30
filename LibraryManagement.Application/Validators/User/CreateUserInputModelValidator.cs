using FluentValidation;
using LibraryManagement.Application.DTOs.InputModels.User;

namespace LibraryManagement.Application.Validators.User;

public class CreateUserInputModelValidator : AbstractValidator<CreateUserInputModel>
{
    public CreateUserInputModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(3, 128).WithMessage("{PropertyName} must be between 3 and 128 characters.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.") 
            .EmailAddress().WithMessage("{PropertyName} must be a valid email address.");
    }
}