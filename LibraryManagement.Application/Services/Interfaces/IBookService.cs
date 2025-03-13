using LibraryManagementSystem.Application.InputModels.Book;
using LibraryManagementSystem.Application.InputModels.User;
using LibraryManagementSystem.Application.ViewModels.Book;

namespace LibraryManagementSystem.Application.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookViewModel>> GetAllBooks();
    Task<BookViewModel?> GetBook(Guid id);
    Task<Guid> CreateBook(CreateBookInputModel model);
}
