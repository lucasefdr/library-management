using LibraryManagementSystem.Core.VOs;

namespace LibraryManagementSystem.Core.Entities;

public sealed class User : BaseEntity
{
    private User() { }
    public User(string name, string email)
    {
        Name = name;
        Email = Email.Create(email);

        LoanList = [];
    }

    public string Name { get; private set; }
    public Email Email { get; private set; }

    #region Navigation Properties/Relacionamentos
    public List<Loan> LoanList { get; private set; }
    #endregion
}
