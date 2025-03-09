namespace LibraryManagementSystem.Core.VOs;
using System.Text.RegularExpressions;

public sealed class Email
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    private Email() { }

    public string Address { get; private set; } = string.Empty;

    private Email(string address)
    {
        Address = address;
    }

    public static Email Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !EmailRegex.IsMatch(address))
        {
            throw new ArgumentException("Invalid email format.", nameof(address));
        }
        return new Email(address);
    }

    public override string ToString() => Address;
}