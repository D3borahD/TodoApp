using System.Text.Json;
using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Encodings.Web;
using BackendApi.Helpers;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private string path = ControllerHelper.GetPath("tasksDatas");

    // Options de sérialisation pour désactiver l'encodage des caractères non ASCII
    public static readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    
    [HttpGet]
    [Route("tasks")] // Route relative à la route de base "api/task"
    public IActionResult GetTasks()
    {
        try
        {
            if (!System.IO.File.Exists(path))
            {
                return NotFound("Aucune tâche trouvée. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(path);
            
            List<Tasks> tasksList = JsonSerializer.Deserialize<List<Tasks>>(json);

            if (tasksList == null || !tasksList.Any())
            {
                return Ok(new List<Tasks>());
            }
            
            return Ok(tasksList);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erreur lors de la récupération des tâches : {e.Message}");
        }
    }
    
    [HttpGet]
    [Route("tasks/{id}")] // Route relative à la route de base "api/task"
    public IActionResult GetTasksById(int id)
    {
        try
        {
            if (!System.IO.File.Exists(path))
            {
                return NotFound("Aucune tâche trouvée. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(path);
            
            List<Tasks> tasksList = JsonSerializer.Deserialize<List<Tasks>>(json);

            if (tasksList == null || !tasksList.Any())
            {
                return Ok(new List<Tasks>());
            }

            foreach (var task in tasksList)
            {
                if (task.Id == id)
                {
                    return Ok(task);
                }
            }

            return StatusCode(204, $"Cette tâche n'existe pas");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erreur lors de la récupération des tâches : {e.Message}");
        }
    }

    [HttpPost]
    [Route("tasks")] // Route relative à la route de base "api/task"
    public IActionResult AddTasks([FromBody] Tasks tasks)
    {
        List<Tasks> tasksList;

        // Vérification du fichier existant
        if (System.IO.File.Exists(path) && new FileInfo(path).Length > 0)
        {
            string json = System.IO.File.ReadAllText(path, Encoding.UTF8);
            tasksList = JsonSerializer.Deserialize<List<Tasks>>(json) ?? new List<Tasks>();
        }
        else
        {
            tasksList = new List<Tasks>();
        }

        // Générer un nouvel ID
        int newId = tasksList.Any() ? tasksList.Max(x => x.Id) + 1 : 1;

        // Ajouter la nouvelle tâche
        Tasks newTasks = new Tasks
        {
            Id = newId,
            Title = tasks.Title,
            IsCompleted = tasks.IsCompleted,
        };
        tasksList.Add(newTasks);

        // Sérialiser les données en JSON
        string updatedJson = JsonSerializer.Serialize(tasksList, options);

        // Écrire le JSON avec encodage explicite UTF-8
        using (var writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            writer.Write(updatedJson);
        }
        
        return StatusCode(201, $"Tâche ajoutée avec succès : Id={newTasks.Id}, Name={newTasks.Title}");
    }
    
    [HttpDelete]
    [Route("tasks/{id}")] // Route relative à la route de base "api/task"
    public IActionResult DeleteTask(int id)
    {
        List<Tasks> tasksList ;
        
        // Récupérer les données
        if (System.IO.File.Exists(path) && new FileInfo(path).Length > 0)
        {
            string json = System.IO.File.ReadAllText(path);
            tasksList = JsonSerializer.Deserialize<List<Tasks>>(json) ?? new List<Tasks>();
            
            foreach (var task in tasksList.ToList())
            {
                if (task.Id == id)
                {
                    tasksList.Remove(task);
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(tasksList, options);
            System.IO.File.WriteAllText(path, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(204, "La tâche à bien été supprimée");
    }
    
    [HttpPatch]
    [Route("tasks/{id}")] // Route relative à la route de base "api/task"
    public IActionResult UpdateTask(int id, Tasks tasks)
    {
        List<Tasks> tasksList ;
        
        // Récupérer les données
        if (System.IO.File.Exists(path) && new FileInfo(path).Length > 0)
        {
            string json = System.IO.File.ReadAllText(path);
            tasksList = JsonSerializer.Deserialize<List<Tasks>>(json) ?? new List<Tasks>();
            
            foreach (var task in tasksList.ToList())
            {
                if (task.Id == id)
                {
                    task.Title = tasks.Title;
                    task.IsCompleted = tasks.IsCompleted;
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(tasksList, options);
            System.IO.File.WriteAllText(path, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(200, $"La tâche {id} a été modifiée");
    }

}