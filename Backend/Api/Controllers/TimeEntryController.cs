using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;


[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class TimeEntryController(ITimeEntryService timeEntryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTimeEntriesAsync()
    {
        var activities = await timeEntryService.GetAllAsync();
        if (!activities.Any()) return NotFound("No activities found");
        return Ok(activities);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> AddActivityAsync([FromBody] TimeEntryCreateDto? timeEntry)
    {
        if (timeEntry is null) return BadRequest("Activity is null");
        var created = await timeEntryService.CreateAsync(timeEntry);
        return Ok(created);
    }

}