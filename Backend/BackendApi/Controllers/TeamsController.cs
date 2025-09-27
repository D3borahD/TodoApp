using System.Text.Json;
using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Encodings.Web;
using BackendApi.Helpers;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private string path = ControllerHelper.GetPath("teamsDatas");

    // Options de sérialisation pour désactiver l'encodage des caractères non ASCII
    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    
    [HttpGet]
    [Route("teams")] // Route relative à la route de base "api/task"
    public IActionResult GetTeams()
    {
        try
        {
            if (!System.IO.File.Exists(path))
            {
                return NotFound("Aucune équipe trouvée. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(path);
            
            List<TeamDao> teamsList = JsonSerializer.Deserialize<List<TeamDao>>(json);

            if (teamsList == null || !teamsList.Any())
            {
                return Ok(new List<TeamDao>());
            }
            
            // todo : récupérer la liste des projet via la table lien équipe projet
            
            return Ok(teamsList);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erreur lors de la récupération des équipes : {e.Message}");
        }
    }
    
    [HttpGet]
    [Route("teams/{id}")] // Route relative à la route de base "api/task"
    public IActionResult GetTeamById(int id)
    {
        try
        {
            if (!System.IO.File.Exists(path))
            {
                return NotFound("Aucune équipe trouvée. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(path);
            
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
    [Route("teams")] // Route relative à la route de base "api/task"
    public IActionResult AddTeam([FromBody] TeamDao teamDao)
    {
        List<TeamDao> teamsList;

        // Vérification du fichier existant
        if (System.IO.File.Exists(path) && new FileInfo(path).Length > 0)
        {
            string json = System.IO.File.ReadAllText(path, Encoding.UTF8);
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
            Name = teamDao.Name,
            Image = teamDao.Image,
        };
        teamsList.Add(newTeamDao);

        // Sérialiser les données en JSON
        string updatedJson = JsonSerializer.Serialize(teamsList, options);

        // Écrire le JSON avec encodage explicite UTF-8
        using (var writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            writer.Write(updatedJson);
        }
        
        return StatusCode(201, $"Équipe ajoutée avec succès : Id={newTeamDao.Id}, Name={newTeamDao.Name}");
    }
    
    [HttpDelete]
    [Route("teams/{id}")] // Route relative à la route de base "api/task"
    public IActionResult DeleteTask(int id)
    {
        List<TeamDao> teamsList ;
        
        // Récupérer les données
        if (System.IO.File.Exists(path) && new FileInfo(path).Length > 0)
        {
            string json = System.IO.File.ReadAllText(path);
            teamsList = JsonSerializer.Deserialize<List<TeamDao>>(json) ?? new List<TeamDao>();
            
            foreach (var task in teamsList.ToList())
            {
                if (task.Id == id)
                {
                    teamsList.Remove(task);
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(teamsList, options);
            System.IO.File.WriteAllText(path, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(204, "L'équipe à bien été supprimée");
    }
    
    [HttpPut]
    [Route("teams/{id}")] // Route relative à la route de base "api/task"
    public IActionResult UpdateTask(int id, TeamDao teamDao)
    {
        List<TeamDao> teamsList ;
        
        // Récupérer les données
        if (System.IO.File.Exists(path) && new FileInfo(path).Length > 0)
        {
            string json = System.IO.File.ReadAllText(path);
            teamsList = JsonSerializer.Deserialize<List<TeamDao>>(json) ?? new List<TeamDao>();
            
            foreach (var team in teamsList.ToList())
            {
                if (team.Id == id)
                {
                    team.Name = teamDao.Name;
                    team.Image = teamDao.Image;
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(teamsList, options);
            System.IO.File.WriteAllText(path, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(200, $"L'équipe {id} a été modifiée");
    }
}