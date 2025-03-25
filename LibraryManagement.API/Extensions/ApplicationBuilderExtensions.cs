using Asp.Versioning.ApiExplorer;

namespace LibraryManagementSystem.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void AddApplicationBuilderConfigurations(this WebApplication app)
    {
        #region Configurações de ambiente de desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // Obtém o IApiVersionDescriptionProvider do provedor de serviços final
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

            app.UseCors("AllowAngularApp");
        }
        #endregion

        #region Configurações da aplicação
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        #endregion
    }
}
