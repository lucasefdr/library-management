using LibraryManagement.Application.DTOs.InputModels.User;
using LibraryManagementSystem.Application.ViewModels.User;

namespace LibraryManagementSystem.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserViewModel>> GetAllUsers();
    Task<UserViewModel?> GetUser(int id);
    Task<int> CreateUser(CreateUserInputModel model);
}
