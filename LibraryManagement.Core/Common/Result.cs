namespace LibraryManagement.Core.Common;

public class Result
{
    protected bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
    public int StatusCode { get; set; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, string errorMessage, int statusCode)
    {
        switch (isSuccess)
        {
            case true when !string.IsNullOrEmpty(errorMessage):
                throw new InvalidOperationException("A successful result cannot contain error.");
            case false when string.IsNullOrEmpty(errorMessage):
                throw new InvalidOperationException("A failure result must contain an error.");
        }

        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        StatusCode = statusCode;
    }

    // Métodos para resultados que não precisam de um retorno
    public static Result Success() => new(true, string.Empty, 200);
    public static Result Failure(string error, int statusCode = 400) => new(false, error, statusCode);

    // Métodos para resultados que precisam de um retorno
    public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    public static Result<T> Failure<T>(string error, int statusCode = 400) => Result<T>.Failure(error, statusCode);
}

public class Result<T> : Result
{
    private readonly T _value;

    public T Value
    {
        get
        {
            // Só há um valor se o resultado for Success
            if (!IsSuccess)
                throw new InvalidOperationException("Cannot access the value of a failed result.");

            return _value;
        }
    }

    // Construtor que passa as informações para a classe base e acrescenta o valor de tipo genérico
    private Result(T value, bool isSuccess, string error, int statusCode) : base(isSuccess, error,
        statusCode)
    {
        _value = value;
    }

    public static Result<T> Success(T value) => new(value, true, string.Empty, 200);
    public new static Result<T> Failure(string error, int statusCode = 400) => new(default!, false, error, statusCode);
}