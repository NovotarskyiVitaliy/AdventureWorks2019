using AdventureWorks2019.Application.Services;
using AdventureWorks2019.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks2019.API.Controllers;

public record LoginRequest(string User, string Password);

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PersonController(IEmployeeService service) : ControllerBase
{
    [HttpGet("GetEmployees")]
    public async Task<IActionResult> GetEmployees(int? dId)
    {
        return Ok(await service.GetEmployees(dId));
    }

    [HttpPost("UpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
    {
        return await service.UpdateEmployee(employee) ? NoContent() : NotFound();
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var token = service.Login(request.User, request.Password);

        return token is null ? Unauthorized() : Ok(new { token });
    }
}
