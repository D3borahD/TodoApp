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
    
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<TeamDto>>> GetTeamsAsync()
    {
        var teams = await _teamService.GetTeamsAsync();

        if (!teams.Any())
            return NotFound("Aucune équipe trouvée.");

        return Ok(teams);
    }
}

    /*[HttpGet]
    [Route("")] 
    public IActionResult GetTeams()
    {
        try
        {
            if (!System.IO.File.Exists(_path))
            {
                return NotFound("Aucune équipe trouvée. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(_path);
            
            List<TeamDao> teamsList = JsonSerializer.Deserialize<List<TeamDao>>(json);

            if (teamsList == null || !teamsList.Any())
            {
                return Ok(new List<TeamDao>());
            }
            
            return Ok(teamsList);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erreur lors de la récupération des équipes : {e.Message}");
        }
    }*/
    
    /*
    [HttpGet]
    [Route("{id}")] 
    public IActionResult GetTeamById(int id)
    {
        try
        {
            if (!System.IO.File.Exists(_path))
            {
                return NotFound("Aucune équipe trouvée. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(_path);
            
            List<TeamDao> teamsList = JsonSerializer.Deserialize<List<TeamDao>>(json);

            if (teamsList == null || !teamsList.Any())
            {
                return Ok(new List<TeamDao>());
            }

            foreach (var team in teamsList)
            {
                if (team.Id == id)
                {
                    return Ok(team);
                }
            }

            return StatusCode(204, $"Cette équipe n'existe pas");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erreur lors de la récupération des équipes : {e.Message}");
        }
    }

    [HttpPost]
    [Route("")] 
    public IActionResult AddTeam([FromBody] TeamDao teamDao)
    {
        List<TeamDao> teamsList;

        // Vérification du fichier existant
        if (System.IO.File.Exists(_path) && new FileInfo(_path).Length > 0)
        {
            string json = System.IO.File.ReadAllText(_path, Encoding.UTF8);
            teamsList = JsonSerializer.Deserialize<List<TeamDao>>(json) ?? new List<TeamDao>();
        }
        else
        {
            teamsList = new List<TeamDao>();
        }

        // Générer un nouvel ID
        int newId = teamsList.Any() ? teamsList.Max(x => x.Id) + 1 : 1;

        // Ajouter la nouvelle équipe
        TeamDao newTeamDao = new TeamDao
        {
            Id = newId,
            Label = teamDao.Label,
        };
        teamsList.Add(newTeamDao);

        // Sérialiser les données en JSON
        string updatedJson = JsonSerializer.Serialize(teamsList, _options);

        // Écrire le JSON avec encodage explicite UTF-8
        using (var writer = new StreamWriter(_path, false, Encoding.UTF8))
        {
            writer.Write(updatedJson);
        }
        
        return StatusCode(201, $"Équipe ajoutée avec succès : Id={newTeamDao.Id}, Label={newTeamDao.Label}");
    }
    
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

