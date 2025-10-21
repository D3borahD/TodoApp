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
    
}

    
    /*
    



    [HttpDelete]
    [Route("{id}")] 
    public IActionResult DeleteTask(int id)
    {
        List<TeamDao> teamsList ;
        
        // Récupérer les données
        if (System.IO.File.Exists(_path) && new FileInfo(_path).Length > 0)
        {
            string json = System.IO.File.ReadAllText(_path);
            teamsList = JsonSerializer.Deserialize<List<TeamDao>>(json) ?? new List<TeamDao>();
            
            foreach (var task in teamsList.ToList())
            {
                if (task.Id == id)
                {
                    teamsList.Remove(task);
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(teamsList, _options);
            System.IO.File.WriteAllText(_path, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(204, "L'équipe à bien été supprimée");
    }
    
    [HttpPut]
    [Route("{id}")] 
    public IActionResult UpdateTask(int id, TeamDao teamDao)
    {
        List<TeamDao> teamsList ;
        
        // Récupérer les données
        if (System.IO.File.Exists(_path) && new FileInfo(_path).Length > 0)
        {
            string json = System.IO.File.ReadAllText(_path);
            teamsList = JsonSerializer.Deserialize<List<TeamDao>>(json) ?? new List<TeamDao>();
            
            foreach (var team in teamsList.ToList())
            {
                if (team.Id == id)
                {
                    team.Label = teamDao.Label;
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(teamsList, _options);
            System.IO.File.WriteAllText(_path, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(200, $"L'équipe {id} a été modifiée");
    }
}
*/

