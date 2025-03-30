using LibraryManagementSystem.Application.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using LibraryManagement.Application.DTOs.InputModels.Book;
using LibraryManagement.Application.ViewModels.Book;

namespace LibraryManagementSystem.Application.Services.Implementations;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<int> CreateBook(CreateBookInputModel model)
    {
        var book = new Book(model.Title, model.Author, model.ISBN, model.PublicationYear);
        await _bookRepository.CreateAsync(book);

        return book.Id;
    }

    public async Task<IEnumerable<BookViewModel>> GetAllBooks()
    {
        var books = await _bookRepository.GetAll()
                                    .Select(book => new BookViewModel(
                                            book.Id,
                                            book.Title,
                                            book.Author,
                                            book.ISBN.Value,
                                            book.Status,
                                            book.PublicationYear))
                                    .ToListAsync();
        return books;
    }

    public async Task<BookViewModel?> GetBook(int id)
    {
        var book = await _bookRepository.FindAsync(id);

        if (book is null) return null;

        return new BookViewModel(book.Id,
                                 book.Title,
                                 book.Author,
                                 book.ISBN.Value,
                                 book.Status,
                                 book.PublicationYear);
    }
}
