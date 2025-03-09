using LibraryManagementSystem.Application.InputModels.Loan;
using LibraryManagementSystem.Application.Services.Interfaces;
using LibraryManagementSystem.Application.ViewModels.Loan;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class LoanController : ControllerBase
{
    private readonly ILoanService _service;

    public LoanController(ILoanService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<LoanViewModel>>> GetAll()
    {
        var loans = await _service.GetAllLoans();

        return Ok(loans);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LoanViewModel>> Get(Guid id)
    {
        var loan = await _service.GetLoan(id);

        return loan is not null ? Ok(loan) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> CreateLoan(CreateLoanInputModel model)
    {
        var id = await _service.CreateLoan(model);

        return CreatedAtAction(nameof(Get), new { id }, model);
    }

    [HttpPut("return/{id}")]
    public async Task<ActionResult> LoanReturn(Guid id)
    {
        await _service.ReturnLoan(id);

        return NoContent();
    }
}
