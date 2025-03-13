using Asp.Versioning;
using LibraryManagementSystem.Application.Services.Implementations;
using LibraryManagementSystem.Application.Services.Interfaces;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Infrastructure.Persistence.Repositories;
using System.Globalization;

namespace LibraryManagementSystem.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServicesConfiguration(this IServiceCollection services)
    {
        #region Configurações da API
        var cultureInfo = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        services.AddControllers(); // Suporte a Controllers
        services.AddEndpointsApiExplorer(); // Endpoints
        #endregion

        #region Injeção de dependências
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IBorrowingRepository, BorrowingRepository>();

        services.AddScoped<IBookService, BookService>()
                        .AddScoped<IUserService, UserService>()
                        .AddScoped<IBorrowingService, BorrowingService>();
        #endregion

        return services;
    }

    public static IServiceCollection AddRoutesConfiguration(this IServiceCollection services)
    {
        #region Configuração de rotas
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        #endregion

        return services;
    }

    public static IServiceCollection AddVersioningConfiguration(this IServiceCollection services)
    {
        #region Configurações de versionamento
        services.AddApiVersioning(opts =>
        {
            opts.DefaultApiVersion = new ApiVersion(1, 0); // Versão padrão
            opts.AssumeDefaultVersionWhenUnspecified = true; // Assume a versão padrão quando não especificada
            opts.ReportApiVersions = true; // Reporta as versões da API
            opts.ApiVersionReader = new UrlSegmentApiVersionReader(); // Lê a versão da API a partir do segmento da URL
        }).AddApiExplorer(opts => // Adiciona o suporte ao Swagger
        {
            opts.GroupNameFormat = "'v'V"; // Formato de exibição das versões
            opts.SubstituteApiVersionInUrl = true; // Substitui a versão da API na URL
        });
        #endregion

        return services;
    }
}
