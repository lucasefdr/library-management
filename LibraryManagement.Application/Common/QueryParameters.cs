namespace LibraryManagement.Application.Common;

public class QueryParameters : PaginationParameters
{
    // Ordenação
    public string SortBy { get; set; } = string.Empty;
    public bool Ascending { get; set; } = true;
    
    // Filtragem dinãmica
    public string SearchTerm { get; set; } = string.Empty;
    public string FilterField { get; set; } = string.Empty;
    public string FilterValue { get; set; } = string.Empty;
}