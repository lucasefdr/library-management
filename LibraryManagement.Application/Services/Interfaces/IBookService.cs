using LibraryManagement.Application.DTOs.InputModels.Book;
using LibraryManagement.Application.ViewModels.Book;
using LibraryManagement.Core.Common;

namespace LibraryManagement.Application.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookViewModel>> GetAllBooks();
    Task<Result<BookViewModel>> GetBook(int id);
    Task<int> CreateBook(CreateBookInputModel model);
}
