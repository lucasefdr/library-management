using LibraryManagementSystem.Application.InputModels.User;
using LibraryManagementSystem.Application.ViewModels.User;

namespace LibraryManagementSystem.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserViewModel>> GetAllUsers();
    Task<UserViewModel?> GetUser(Guid id);
    Task<Guid> CreateUser(CreateUserInputModel model);
}
