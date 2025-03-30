namespace LibraryManagement.Application.DTOs.InputModels.User;

public record CreateUserInputModel(
    string Name,
    string Email);