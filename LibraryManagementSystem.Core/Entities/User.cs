namespace LibraryManagementSystem.Core.Entities;

public sealed class User : BaseEntity
{
    public User(string name, string email)
    {
        Name = name;
        Email = email;

        LoanList = [];
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public List<Loan> LoanList { get; private set; }
}
