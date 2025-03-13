using LibraryManagementSystem.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServicesConfiguration();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddRoutesConfiguration();
builder.Services.AddVersioningConfiguration();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();
app.AddApplicationBuilderConfigurations();
app.Run();
