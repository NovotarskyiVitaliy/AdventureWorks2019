using AdventureWorks2019.Application.Data;
using AdventureWorks2019.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks2019.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController(ILogger<PersonController> logger, IDataEmployeeService service) : ControllerBase
{
    [HttpGet(Name = "GetPersons")]
    public async Task<IActionResult> GetAsync(int? dId)
    {
        return Ok(await service.GetEmployee(dId));
    }
        
    [HttpPost(Name = "PutPersons")]
    public async Task<string> PutAsync(Employee employee)
    {
        await service.Update(employee);

        return await Task.FromResult("ok");
    }
}