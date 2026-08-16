using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class StepsController(IBaseService<StepDto> baseService) : ControllerBase
{
    [HttpPost("")]
    public async Task<ActionResult<StepDto>> CreateStepAsync([FromBody] StepDto? stepDto)
    {
        if(stepDto is null) return BadRequest("The step is null");
        if(string.IsNullOrEmpty(stepDto.Label)) return BadRequest("The step label is null");
        var step = await baseService.CreateAsync(stepDto);
        return Ok(step);
    }

}