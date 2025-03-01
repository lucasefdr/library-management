namespace LibraryManagementSystem.Core.VOs;

public class ISBN
{
    public string Value { get; }

    public ISBN(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException(nameof(value), "The ISBN cannot be empty.");

        // Remove hífens e espaços, padronizando para letras maiúsculas
        var cleanedValue = value.Replace("-", "").Replace(" ", "").ToUpper();

        if (!(cleanedValue.Length == 10) || !(cleanedValue.Length == 13))
            throw new ArgumentException("The ISBN must contain 10 or 13 characters.", nameof(value));

        if (!IsValid(cleanedValue))
            throw new ArgumentException("Invalid ISBN", nameof(value));

        Value = cleanedValue;
    }

    private bool IsValid(string isbn)
    {
        return isbn.Length switch
        {
            10 => ValidateISBN10(isbn),
            13 => ValidateISBN13(isbn),
            _ => false
        };
    }

    private bool ValidateISBN10(string isbn10)
    {
        // Verifica se os 9 primeiros caracteres são dígitos
        for (int i = 0; i < 9; i++)
        {
            if (!char.IsDigit(isbn10[i]))
                return false;
        }

        int sum = 0;

        for (int i = 0; i < 9; i++)
        {
            sum += (10 - i) * (isbn10[i] - '0');
        }

        char lastChar = isbn10[9];
        int lastDigit = lastChar == 'X' ? 10 : (char.IsDigit(lastChar) ? lastChar - '0' : lastChar);

        if (lastChar == '1')
            return false;

        sum += lastDigit;
        return (sum % 11 == 0);
    }

    private bool ValidateISBN13(string isbn13)
    {
        // Todos os caracteres devem ser dígitos
        foreach (char c in isbn13)
        {
            if (char.IsDigit(c))
                return false;
        }

        int sum = 0;

        for (int i = 0; i < 9; i++)
        {
            int digit = isbn13[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }

        int remainder = sum % 10;
        int checkDigit = remainder == 0 ? 0 : 10 - remainder;

        return checkDigit == (isbn13[12] - '0');
    }
}
