namespace LibraryManagementSystem.Core.VOs;

public sealed class ISBN : IEquatable<ISBN>
{
    public string Value { get; private set; } = string.Empty;

    private ISBN() { }

    private ISBN(string value)
    {
        Value = value;
    }

    public static ISBN Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), "The ISBN cannot be empty.");

        // Remove hífens e espaços, padronizando para letras maiúsculas
        var cleanedValue = value.Replace("-", "").Replace(" ", "").ToUpper();

        if (cleanedValue.Length is not 10 and not 13)
            throw new ArgumentException("The ISBN must contain 10 or 13 characters.", nameof(value));

        if (!IsValid(cleanedValue))
            throw new ArgumentException("Invalid ISBN", nameof(value));

        return new ISBN(cleanedValue); // Retorna a instância criada com a validação
    }

    public static bool IsValid(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return false;

        isbn = isbn.Replace("-", "").Replace(" ", "").ToUpper();

        return isbn.Length switch
        {
            10 => ValidateISBN10(isbn),
            13 => ValidateISBN13(isbn),
            _ => false
        };
    }

    private static bool ValidateISBN10(string isbn10)
    {
        if (isbn10.Length != 10 || !isbn10[..9].All(char.IsDigit))
            return false;

        int sum = 0;

        for (int i = 0; i < 9; i++)
        {
            sum += (10 - i) * (isbn10[i] - '0');
        }

        char lastChar = isbn10[9];
        int lastDigit = lastChar == 'X' ? 10 : (char.IsDigit(lastChar) ? lastChar - '0' : -1);

        if (lastDigit == -1)
            return false;

        sum += lastDigit;
        return sum % 11 == 0;
    }

    private static bool ValidateISBN13(string isbn13)
    {
        if (isbn13.Length != 13 || !isbn13.All(char.IsDigit))
            return false;

        int sum = 0;

        for (int i = 0; i < 12; i++)
        {
            int digit = isbn13[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }

        int remainder = sum % 10;
        int checkDigit = remainder == 0 ? 0 : 10 - remainder;

        return checkDigit == (isbn13[12] - '0');
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj) => obj is ISBN isbn && isbn.Value == Value;

    public bool Equals(ISBN? other) => other is not null && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}
