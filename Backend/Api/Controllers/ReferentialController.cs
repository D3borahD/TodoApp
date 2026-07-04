using Application.Interfaces;
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
}