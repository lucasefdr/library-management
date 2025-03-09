using LibraryManagementSystem.Application.InputModels.Borrowing;
using LibraryManagementSystem.Application.Services.Interfaces;
using LibraryManagementSystem.Application.ViewModels.Borrowing;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class BorrowingController : ControllerBase
{
    private readonly IBorrowingService _service;

    public BorrowingController(IBorrowingService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<BorrowingViewModel>>> GetAll()
    {
        var borrowings = await _service.GetAllBorrowings();

        return Ok(borrowings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BorrowingViewModel>> Get(Guid id)
    {
        var borrowing = await _service.GetBorrowing(id);

        return borrowing is not null ? Ok(borrowing) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateBorrowingInputModel model)
    {
        var id = await _service.CreateBorrowing(model);

        return CreatedAtAction(nameof(Get), new { id }, model);
    }

    [HttpPut("return/{id}")]
    public async Task<ActionResult> LoanReturn(Guid id)
    {
        await _service.ReturnBorrowing(id);

        return NoContent();
    }
}
