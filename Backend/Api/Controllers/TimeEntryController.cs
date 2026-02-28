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
    
    [HttpGet("{date}")]
    public async Task<IActionResult> GetByCurrentMonth(DateTime date)
    {
        var result = await timeEntryService.GetByCurrentMonthAsync(date);
        return Ok(result);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateActivityAsync(int id, [FromBody] TimeEntryCreateDto? timeEntry)
    {
        if (timeEntry is null) return BadRequest("Activity is null");
        
        timeEntry. Id = id;
        var updated = await timeEntryService.UpdateAsync(timeEntry);
        
        return updated is null ? NotFound("TimeEntry could not be updated") : Ok(updated);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddTimeEntryAsync([FromBody] TimeEntryCreateDto? timeEntry)
    {
        if (timeEntry is null) return BadRequest("Activity is null");
        var created = await timeEntryService.CreateAsync(timeEntry);
        return Ok(created);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTimeEntryAsync(int id)
    {
        bool isDeleted = await timeEntryService.DeleteAsync(id);
        return isDeleted ? NoContent() : NotFound("TimeEntry not found");
    }
}