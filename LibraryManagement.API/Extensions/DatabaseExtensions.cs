using LibraryManagementSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        #region Configurações do banco de dados
        var connectionString = configuration.GetConnectionString("LMSDB");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        #endregion

        return services;
    }
}
