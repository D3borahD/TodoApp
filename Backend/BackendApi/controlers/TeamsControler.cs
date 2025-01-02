using System.Text.Json;
using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Encodings.Web;
using System.IO;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private static readonly string dataBasePath = "/Users/deborah/Documents/dev/TodoApp/Backend";
    private readonly string teamsdatasPath = $"{dataBasePath}/teamsDatas";
    
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
            if (!System.IO.File.Exists(teamsdatasPath))
            {
                return NotFound("Aucune équipe trouvée. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(teamsdatasPath);
            
            List<Teams> teamsList = JsonSerializer.Deserialize<List<Teams>>(json);

            if (teamsList == null || !teamsList.Any())
            {
                return Ok(new List<Teams>());
            }
            
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
            if (!System.IO.File.Exists(teamsdatasPath))
            {
                return NotFound("Aucune équipe trouvée. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(teamsdatasPath);
            
            List<Teams> teamsList = JsonSerializer.Deserialize<List<Teams>>(json);

            if (teamsList == null || !teamsList.Any())
            {
                return Ok(new List<Teams>());
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
    public IActionResult AddTeam([FromBody] Teams teams)
    {
        List<Teams> teamsList;

        // Vérification du fichier existant
        if (System.IO.File.Exists(teamsdatasPath) && new FileInfo(teamsdatasPath).Length > 0)
        {
            string json = System.IO.File.ReadAllText(teamsdatasPath, Encoding.UTF8);
            teamsList = JsonSerializer.Deserialize<List<Teams>>(json) ?? new List<Teams>();
        }
        else
        {
            teamsList = new List<Teams>();
        }

        // Générer un nouvel ID
        int newId = teamsList.Any() ? teamsList.Max(x => x.Id) + 1 : 1;

        // Ajouter la nouvelle tâche
        Teams newTeam = new Teams
        {
            Id = newId,
            Name = teams.Name,
            Image = teams.Image,
        };
        teamsList.Add(newTeam);

        // Sérialiser les données en JSON
        string updatedJson = JsonSerializer.Serialize(teamsList, options);

        // Écrire le JSON avec encodage explicite UTF-8
        using (var writer = new StreamWriter(teamsdatasPath, false, Encoding.UTF8))
        {
            writer.Write(updatedJson);
        }
        
        return StatusCode(201, $"Équipe ajoutée avec succès : Id={newTeam.Id}, Name={newTeam.Name}");
    }
    
    [HttpDelete]
    [Route("teams/{id}")] // Route relative à la route de base "api/task"
    public IActionResult DeleteTask(int id)
    {
        List<Teams> teamsList ;
        
        // Récupérer les données
        if (System.IO.File.Exists(teamsdatasPath) && new FileInfo(teamsdatasPath).Length > 0)
        {
            string json = System.IO.File.ReadAllText(teamsdatasPath);
            teamsList = JsonSerializer.Deserialize<List<Teams>>(json) ?? new List<Teams>();
            
            foreach (var task in teamsList.ToList())
            {
                if (task.Id == id)
                {
                    teamsList.Remove(task);
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(teamsList, options);
            System.IO.File.WriteAllText(teamsdatasPath, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(204, "L'équipe à bien été supprimée");
    }
    
    [HttpPut]
    [Route("teams/{id}")] // Route relative à la route de base "api/task"
    public IActionResult UpdateTask(int id, Teams teams)
    {
        List<Teams> teamsList ;
        
        // Récupérer les données
        if (System.IO.File.Exists(teamsdatasPath) && new FileInfo(teamsdatasPath).Length > 0)
        {
            string json = System.IO.File.ReadAllText(teamsdatasPath);
            teamsList = JsonSerializer.Deserialize<List<Teams>>(json) ?? new List<Teams>();
            
            foreach (var team in teamsList.ToList())
            {
                if (team.Id == id)
                {
                    team.Name = teams.Name;
                    team.Image = teams.Image;
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(teamsList, options);
            System.IO.File.WriteAllText(teamsdatasPath, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(200, $"L'équipe {id} a été modifiée");
    }
}