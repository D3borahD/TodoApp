using Application.Interfaces;
using Domain.DTO;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ReferentialController(IReferentialService referentialService) : ControllerBase
{
    [HttpGet("status")]
    public async Task<ActionResult<List<Status>>> GetStatusAsync()
    {
        var status =  await referentialService.GetStatusAsync();
        return Ok(status);
    }
    
    [HttpGet("projectTypes")]
    public async Task<ActionResult<List<ProjectTypesDto>>> GetCountriesAsync()
    {
        var countries = await referentialService.GetProjectTypeAsync();
        return Ok(countries);
    }
}