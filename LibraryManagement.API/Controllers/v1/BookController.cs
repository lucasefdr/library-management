using LibraryManagement.Application.Common;
using LibraryManagement.Application.DTOs.InputModels.Book;
using LibraryManagement.Application.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
[DisableCors]
public class BookController(IBookService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<Book>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var books = await service.GetAllBooks(parameters);
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> Get(int id)
    {
        var result = await service.GetBook(id);
        return result.IsFailure
            ? StatusCode(result.StatusCode, result.ErrorMessage)
            : Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateBookInputModel model)
    {
        var id = await service.CreateBook(model);
        return CreatedAtAction(nameof(Get), new { id }, model);
    }
}