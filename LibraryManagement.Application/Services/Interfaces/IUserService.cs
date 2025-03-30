using LibraryManagement.Application.Common;
using LibraryManagement.Application.DTOs.InputModels.User;
using LibraryManagement.Core.Common;
using LibraryManagementSystem.Application.ViewModels.User;

namespace LibraryManagement.Application.Services.Interfaces;

public interface IUserService
{
    Task<PagedResult<UserViewModel>> GetAllUsers(QueryParameters parameters);
    Task<Result<UserViewModel>> GetUser(int id);
    Task<int> CreateUser(CreateUserInputModel model);
}
