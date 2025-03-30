namespace LibraryManagement.Application.Common;

public class PaginationParameters
{
    private const int MaxPageSize = 50; // Tamanho máximo 
    private int _pageSize = 10; // Tamanho padráo
    
    public int PageNumber { get; set; } = 1; // Página inicial

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value; 
    }
}