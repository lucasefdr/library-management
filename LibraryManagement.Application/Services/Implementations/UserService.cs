using LibraryManagement.Application.Common;
using LibraryManagement.Application.DTOs.InputModels.User;
using LibraryManagement.Application.Repositories;
using LibraryManagement.Application.Services.Interfaces;
using LibraryManagement.Core.Common;
using LibraryManagementSystem.Application.ViewModels.User;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagement.Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> CreateUser(CreateUserInputModel model)
    {
        var user = new User(model.Name, model.Email);

        await _userRepository.CreateAsync(user);

        return user.Id;
    }

    public async Task<PagedResult<UserViewModel>> GetAllUsers(QueryParameters parameters)
    {
        var users = await _userRepository.ReadAllAsync(parameters);
        
        var response = new PagedResult<UserViewModel>()
        {
            CurrentPage = users.CurrentPage,
            PageSize = users.PageSize,
            TotalCount = users.TotalCount,
            TotalPages = users.TotalPages,
            Items =
            [
                ..users.Items.Select(u =>
                    new UserViewModel(u.Name, u.Email.Address))
            ]
        };

        return response;
    }

    public async Task<Result<UserViewModel>> GetUser(int id)
    {
        var user = await _userRepository.FindAsync(id);

        if (user == null)
            return Result.Failure<UserViewModel>("User not found", 404);

        var response = new UserViewModel(user.Name, user.Email.Address);
        
        return Result.Success(response);
    }
}
