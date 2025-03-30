using LibraryManagement.Application.DTOs.InputModels.Book;
using LibraryManagement.Application.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
[DisableCors]
public class BookController : ControllerBase
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetAll()
    {
        var books = await _service.GetAllBooks();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> Get(int id)
    {
        var result = await _service.GetBook(id);
        return result.IsFailure
            ? StatusCode(result.StatusCode, result.ErrorMessage)
            : Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateBookInputModel model)
    {
        var id = await _service.CreateBook(model);
        return CreatedAtAction(nameof(Get), new { id }, model);
    }
}