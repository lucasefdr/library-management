using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{

    public BookRepository(AppDbContext context) : base(context)
    {
    }
}
