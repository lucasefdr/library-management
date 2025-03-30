using LibraryManagement.Application.DTOs.InputModels.Book;
using LibraryManagement.Application.ViewModels.Book;

namespace LibraryManagementSystem.Application.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookViewModel>> GetAllBooks();
    Task<BookViewModel?> GetBook(int id);
    Task<int> CreateBook(CreateBookInputModel model);
}
