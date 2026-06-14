using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;


[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ReferentialController(IReferentialService referentialService) : ControllerBase
{
    [HttpGet("status")]
    public async Task<ActionResult<List<StatusDto>>> GetStatusAsync()
    {
        var status =  await referentialService.GetStatusAsync();
        return Ok(status);
    }
    
  
}