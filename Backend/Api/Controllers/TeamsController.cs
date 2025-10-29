using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.DTO;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class TeamsController(IBaseService<TeamDto> teamService) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<List<TeamDto>>> GetTeamsAsync()
    {
        var teams = await teamService.GetAllAsync();
        
        if (!teams.Any() || teams.Count == 0)
            return NotFound("No team found.");
        
        return Ok(teams);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TeamDto>> GetTeamsByIdAsync(int id)
    {
        TeamDto team = await teamService.GetByIdAsync(id);

        if (team == null) 
            return NotFound($"Team id {id} does not exist.");
        
        return Ok(team);
    }

    [HttpPost("")]
    public async Task<ActionResult<TeamDto>> CreateAsync([FromBody] TeamDto teamDto)
    {
        if (teamDto == null)
            return BadRequest("The team data is invalid.");
        
        TeamDto createdTeam = await teamService.CreateAsync(teamDto);
        
        if (createdTeam == null)
            return StatusCode(500, "Error while creating the team.");
        
        return Ok(createdTeam);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TeamDto>> UpdateAsync(int id, [FromBody] TeamDto teamDto)
    {
        if (teamDto == null)
            return BadRequest("The team data is invalid.");
        teamDto.Id = id;
        
        TeamDto updatedTeam = await teamService.UpdateAsync(teamDto);
       
        if (updatedTeam == null)
           return NotFound($"Team id {id} does not exist.");
        
        return Ok(updatedTeam);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeamAsync(int id)
    {
        bool isDeleted = await teamService.DeleteAsync(id);
        
        if (!isDeleted)
            return NotFound($"Team with ID {id} does not exist.");
        
        return NoContent();
    }
}


