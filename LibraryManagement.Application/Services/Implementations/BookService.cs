using LibraryManagement.Application.Common;
using LibraryManagement.Application.DTOs.InputModels.Book;
using LibraryManagement.Application.Repositories;
using LibraryManagement.Application.Services.Interfaces;
using LibraryManagement.Application.ViewModels.Book;
using LibraryManagement.Core.Common;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagement.Application.Services.Implementations;

public class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task<int> CreateBook(CreateBookInputModel model)
    {
        var book = new Book(model.Title, model.Author, model.ISBN, model.PublicationYear);
        await bookRepository.CreateAsync(book);

        return book.Id;
    }

    public async Task<PagedResult<BookViewModel>> GetAllBooks(QueryParameters parameters)
    {
        var books = await bookRepository.ReadAllAsync(parameters);

        var response = new PagedResult<BookViewModel>()
        {
            CurrentPage = books.CurrentPage,
            PageSize = books.PageSize,
            TotalCount = books.TotalCount,
            TotalPages = books.TotalPages,
            Items =
            [
                ..books.Items.Select(b =>
                    new BookViewModel(b.Id, b.Title, b.Author, b.ISBN.ToString(), b.Status, b.PublicationYear))
            ]
        };

        return response;
    }

    public async Task<Result<BookViewModel>> GetBook(int id)
    {
        var book = await bookRepository.ReadAsync(id);

        if (book is null)
            return Result.Failure<BookViewModel>("Book not found", 404);

        var response = new BookViewModel(book.Id,
            book.Title,
            book.Author,
            book.ISBN.Value,
            book.Status,
            book.PublicationYear);

        return Result.Success(response);
    }
}