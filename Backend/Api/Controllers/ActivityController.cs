using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ActivityController(IBaseService<ActivityDto> activityService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetActivitiesAsync()
    {
        var activities = await activityService.GetAllAsync();
        if (!activities.Any()) return NotFound("No activities found");
        return Ok(activities);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetActivityByIdAsync(int id)
    {
        var activity = await activityService.GetByIdAsync(id);
        return Ok(activity);
    }

    [HttpPost]
    public async Task<IActionResult> AddActivityAsync([FromBody] ActivityDto? activity)
    {
        if (activity is null) return BadRequest("Activity is null");
        var created = await activityService.CreateAsync(activity);
        return Ok(created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateActivityAsync(int id, [FromBody] ActivityDto? activity)
    {
        if (activity is null) return BadRequest("Activity is null");
        
        activity. Id = id;
        var updated = await activityService.UpdateAsync(activity);
        return updated is null ? NotFound("Activity could not be updated") : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteActivityAsync(int id)
    {
        bool isDeleted = await activityService.DeleteAsync(id);
        return isDeleted ? NoContent() : NotFound("Activity not found");
    }
}