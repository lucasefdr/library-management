using LibraryManagement.Application.Common;
using LibraryManagement.Application.DTOs.InputModels.Borrowing;
using LibraryManagement.Application.Services.Interfaces;
using LibraryManagementSystem.Application.ViewModels.Borrowing;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class BorrowingController(IBorrowingService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<BorrowingViewModel>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var borrowings = await service.GetAllBorrowings(parameters);

        return Ok(borrowings);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BorrowingViewModel>> Get(int id)
    {
        var result = await service.GetBorrowing(id);

        return result.IsFailure
            ? StatusCode(result.StatusCode, result.ErrorMessage)
            : Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateBorrowingInputModel model)
    {
        var result = await service.CreateBorrowing(model);

        return result.IsFailure
            ? StatusCode(result.StatusCode, result.ErrorMessage)
            : CreatedAtAction(nameof(Get), new { v = "1.0", id = result.Value }, model);
    }

    [HttpPut("{id:int}/return")]
    public async Task<ActionResult> ReturnBorrowing(int id)
    {
        var result = await service.ReturnBorrowing(id);

        return result.IsFailure
            ? StatusCode(result.StatusCode, result.ErrorMessage)
            : NoContent();
    }
}