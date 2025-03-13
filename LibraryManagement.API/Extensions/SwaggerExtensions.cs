using Microsoft.OpenApi.Models;

namespace LibraryManagementSystem.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        #region Swagger
        services.AddSwaggerGen(options =>
        {
            // Cria documentos Swagger para v1
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Library Management API",
                Version = "v1",
                Description = "Documentação da API - Versão 1"
            });
        });
        #endregion

        return services;
    }
}
