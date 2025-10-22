using System.Text.Json;
using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Encodings.Web;
using Application.Interfaces;
using BackendApi.Helpers;
using Domain.DTO;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class TeamsController(ITeamService _teamService) : ControllerBase
{
    
    [HttpGet("")]
    public async Task<ActionResult<List<TeamDto>>> GetTeamsAsync()
    {
        var teams = await _teamService.GetTeamsAsync();

        if (!teams.Any())
            return NotFound("No team found.");

        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamDto>> GetTeamsByIdAsync(int id)
    {
        TeamDto team = await _teamService.GetTeamsByIdAsync(id);

        if (team == null)
        {
            return NotFound($"Team id {id} does not exist.");
        }
        
        return Ok(team);
    }

    [HttpPost("")]
    public async Task<ActionResult<TeamDto>> CreateTeamAsync([FromBody] TeamDto teamDto)
    {
        if (teamDto == null)
            return BadRequest("The team data is invalid.");
        
        TeamDto createdTeam = await _teamService.CreateTeamAsync(teamDto);
        
        if (createdTeam == null)
            return StatusCode(500, "Error while creating the team.");
        
        return Ok(createdTeam);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TeamDto>> UpdateTeamAsync(int id, [FromBody] TeamDto teamDto)
    {
        if (teamDto == null)
            return BadRequest("The team data is invalid.");
        teamDto.Id = id;
        
        TeamDto updatedTeam = await _teamService.UpdateTeamAsync(teamDto);
       
        if (updatedTeam == null)
           return NotFound($"Team id {id} does not exist.");
        
        return Ok(updatedTeam);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeamAsync(int id)
    {
        bool deleted = await _teamService.DeleteTeamAsync(id);
        
        if (!deleted)
            return NotFound($"Team with ID {id} does not exist.");
        
        return NoContent();
    }
}


