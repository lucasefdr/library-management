using System.Linq.Expressions;
using System.Reflection;
using LibraryManagement.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Extensions;

public static class QueryableExtensions
{
    // Paginação assíncrona
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>
        (this IQueryable<T> source, PaginationParameters parameters)
    {
        // Total
        var countAsync = await source.CountAsync();

        // Skip, Take
        var items = await
            source.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

        var pagedResult = new PagedResult<T>
        {
            CurrentPage = parameters.PageNumber,
            PageSize = parameters.PageSize,
            TotalCount = countAsync,
            TotalPages = (int)Math.Ceiling((double)countAsync / parameters.PageSize),
            Items = items,
        };

        return pagedResult;
    }

    // Ordenação dinâmica
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, string sortBy, bool ascending = true)
    {
        // Valida se tem alguma parâmetro para ordenação
        if (string.IsNullOrWhiteSpace(sortBy))
            return source;

        // Obtém as propriedades da entidade
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Verifica se a propriedade passada existe
        var propertyInfo = properties.FirstOrDefault(p => p.Name.Equals(sortBy, StringComparison.OrdinalIgnoreCase));

        // Se não existir, retorna a query
        if (propertyInfo == null)
            return source;

        // Constrói expressão lambda para ordenação
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyInfo);
        var lambda = Expression.Lambda(property, parameter);

        // Define o método de ordenação
        var methodName = ascending ? "OrderBy" : "OrderByDescending";

        // Cria uma chamada de ordenação
        var methodCall = Expression.Call(
            typeof(Queryable),
            methodName,
            [typeof(T), propertyInfo.PropertyType],
            source.Expression,
            Expression.Quote(lambda));

        // Aplica ao LINQ
        return source.Provider.CreateQuery<T>(methodCall);
    }

    // Filtragem chave : valor
    public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> source, string filterField, string filterValue)
    {
        if (string.IsNullOrEmpty(filterField) || string.IsNullOrEmpty(filterValue))
            return source;

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var propertyInfo =
            properties.FirstOrDefault(p => p.Name.Equals(filterField, StringComparison.OrdinalIgnoreCase));

        if (propertyInfo == null)
            return source;

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyInfo);

        Expression filterExpression;

        // Dependendo do tipo da propriedade, construímos expressões diferentes
        if (propertyInfo.PropertyType == typeof(string))
        {
            var constant = Expression.Constant(filterValue, typeof(string));

            // Verifica se a string contém o valor de filtro (case insensitive)
            var method = typeof(string).GetMethod("Contains", [typeof(string)]);

            filterExpression = Expression.Call(property, method!, constant);
        }
        else if (propertyInfo.PropertyType == typeof(int) && int.TryParse(filterValue, out int intValue))
        {
            var constant = Expression.Constant(intValue, typeof(int));
            filterExpression = Expression.Equal(property, constant);
        }
        else if (propertyInfo.PropertyType == typeof(DateTime) &&
                 DateTime.TryParse(filterValue, out DateTime dateValue))
        {
            var constant = Expression.Constant(dateValue, typeof(DateTime));
            filterExpression = Expression.Equal(property, constant);
        }
        else if (propertyInfo.PropertyType == typeof(bool) && bool.TryParse(filterValue, out bool boolValue))
        {
            var constant = Expression.Constant(boolValue, typeof(bool));
            filterExpression = Expression.Equal(property, constant);
        }
        else
        {
            // Se não conseguirmos construir um filtro válido, retornamos a consulta original
            return source;
        }

        // Criando a expressão lambda completa
        var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);

        // Aplicando o filtro usando Where
        return source.Where(lambda);
    }

    // Método de extensão para busca em várias propriedades de texto
    public static IQueryable<T> ApplySearch<T>(this IQueryable<T> query, string searchTerm,
        params string[] propertyNames)
    {
        // Valida se o método de busca e as propriedades existem, caso não, retorna a query inicial
        if (string.IsNullOrEmpty(searchTerm) || propertyNames.Length == 0)
            return query;

        // Obtém as propriedades do tipo T públicas e de instância
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Cria um parâmetro para expressão lambda
        var parameter = Expression.Parameter(typeof(T), "x");

        // Iniciamos com uma expressão "false" para construir as OR conditions
        Expression combinedExpression = Expression.Constant(false);

        // Método Contains da classe string
        var containsMethod = typeof(string).GetMethod("Contains", [typeof(string)]);
        var searchConstant = Expression.Constant(searchTerm, typeof(string));

        foreach (var propName in propertyNames)
        {
            // Verifica se a propriedade existe e é do tipo string
            var propertyInfo = properties.FirstOrDefault(p =>
                p.Name.Equals(propName, StringComparison.OrdinalIgnoreCase) &&
                p.PropertyType == typeof(string));

            if (propertyInfo == null) continue;

            // Cria uma expressão para acessar a propriedade do objeto
            var property = Expression.Property(parameter, propertyInfo);

            // Verifica se a propriedade não é null antes de chamar Contains
            var notNullCheck = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));

            // Cria a chamada para o método Contains
            var containsCall = Expression.Call(property, containsMethod!, searchConstant);

            // Combina a verificação de null com a chamada de Contains
            var safeContainsCall = Expression.AndAlso(notNullCheck, containsCall);

            // Adiciona esta condição ao combinedExpression usando OR (||)
            combinedExpression = Expression.OrElse(combinedExpression, safeContainsCall);
        }

        // Cria a expressão lambda final
        var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);

        // Aplica o filtro
        return query.Where(lambda);
    }

    // Método de extensão que aplica paginação, ordenação e filtragem de uma vez
    public static IQueryable<T> ApplyQueryParameters<T>(this IQueryable<T> query, QueryParameters parameters,
        string[] searchableProperties = null!)
    {
        // Aplica o filtro de busca, se houver
        if (!string.IsNullOrWhiteSpace(parameters.SearchTerm) && searchableProperties is { Length: > 0 })
        {
            query = query.ApplySearch(parameters.SearchTerm, searchableProperties);
        }

        // Aplica o filtro específico, se houver
        if (!string.IsNullOrWhiteSpace(parameters.FilterField) && !string.IsNullOrWhiteSpace(parameters.FilterValue))
        {
            query = query.ApplyFiltering(parameters.FilterField, parameters.FilterValue);
        }

        // Aplica a ordenação, se houver
        if (!string.IsNullOrWhiteSpace(parameters.SortBy))
        {
            query = query.ApplySorting(parameters.SortBy, parameters.Ascending);
        }

        return query;
    }
}