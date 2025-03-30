namespace LibraryManagementSystem.Application.ViewModels.User;

public class UserViewModel
{
    public UserViewModel(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public string Name { get; set; }
    public string Email { get; set; }
}
