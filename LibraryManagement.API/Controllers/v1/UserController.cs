using LibraryManagementSystem.Application.InputModels.User;
using LibraryManagementSystem.Application.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        var users = await _service.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(Guid id)
    {
        var user = await _service.GetUser(id);

        return user is not null ? Ok(user) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateUserInputModel model)
    {
        var id = await _service.CreateUser(model);

        return CreatedAtAction(nameof(Get), new { id }, model);
    }
}
