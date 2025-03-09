using LibraryManagementSystem.Application.InputModels.User;
using LibraryManagementSystem.Application.Services.Interfaces;
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

    public async Task<Guid> CreateUser(CreateUserInputModel model)
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

    public async Task<UserViewModel?> GetUser(Guid id)
    {
        var user = await _userRepository.FindAsync(id);

        if (user is null) return null;

        return new UserViewModel(user.Name, user.Email.Address);
    }
}
