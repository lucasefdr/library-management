using LibraryManagement.Application.DTOs.InputModels.User;
using LibraryManagement.Core.Common;
using LibraryManagementSystem.Application.ViewModels.User;

namespace LibraryManagement.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserViewModel>> GetAllUsers();
    Task<Result<UserViewModel>> GetUser(int id);
    Task<int> CreateUser(CreateUserInputModel model);
}
