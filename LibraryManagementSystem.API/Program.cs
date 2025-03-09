using LibraryManagementSystem.Application.Services.Implementations;
using LibraryManagementSystem.Application.Services.Interfaces;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.Infrastructure;
using LibraryManagementSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura��o do banco de dados
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("test"), ServiceLifetime.Singleton);

// Configura��o das inje��es de depend�ncias
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ILoanRepository, LoanRepository>();

builder.Services.AddScoped<IBookService, BookService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<ILoanService, LoanService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
