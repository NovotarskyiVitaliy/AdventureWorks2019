using AdventureWorks2019.Application;
using AdventureWorks2019.Application.Services;
using AdventureWorks2019.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AdventureWorks2019.API.Controllers;

[Route("api/[controller]")]

public class PersonController(ILogger<PersonController> logger, IEmployeeService service) : ControllerBase
{
    [HttpGet]
    [Route("GetEmployees")]
    [Authorize]
    public async Task<IActionResult> GetEmployees(int? dId)
    {
        return Ok(await service.GetEmployees(dId));
    }

    [HttpPost]
    [Route("UpdateEmployee")]
    public async Task<string> UpdateEmployee([FromBody]Employee employee)
    {
        await service.UpdateEmployee(employee);

        return await Task.FromResult("ok");
    }

    [HttpPost]
    [Route("Login")]
    public IResult Login(string user, string password)
    {
        var token =  service.Login(user, new PasswordHasher().Generate(password));

        HttpContext.Response.Cookies.Append("myToken", token);

        return Results.Ok(token);
    }
}