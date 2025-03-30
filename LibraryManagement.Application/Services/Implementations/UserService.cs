using LibraryManagement.Application.DTOs.InputModels.User;
using LibraryManagement.Application.Services.Interfaces;
using LibraryManagement.Core.Common;
using LibraryManagementSystem.Application.ViewModels.User;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Application.Services.Implementations;

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

    public async Task<IEnumerable<UserViewModel>> GetAllUsers()
    {
        var users = await _userRepository.GetAll()
                                        .Select(user => new UserViewModel(user.Name, user.Email.Address))
                                        .ToListAsync();

        return users;
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
