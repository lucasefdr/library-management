using LibraryManagement.Application.Common;
using LibraryManagement.Application.DTOs.InputModels.User;
using LibraryManagement.Application.Services.Interfaces;
using LibraryManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<User>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var users = await service.GetAllUsers(parameters);
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> Get(int id)
    {
        var result = await service.GetUser(id);

        return result.IsFailure 
            ? StatusCode(result.StatusCode, result.ErrorMessage)
            : Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateUserInputModel model)
    {
        var id = await service.CreateUser(model);

        return CreatedAtAction(nameof(Get), new { id }, model);
    }
}
