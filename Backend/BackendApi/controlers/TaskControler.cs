using System.IO;
using System.Text.Json;
using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace BackendApi.Controllers;



[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly string dataBasePath = "/Users/deborah/Documents/dev/TodoApp/Backend/datas";
    
    // Options de sérialisation pour désactiver l'encodage des caractères non ASCII
    private JsonSerializerOptions options = new JsonSerializerOptions
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
            if (!System.IO.File.Exists(dataBasePath))
            {
                return NotFound("Aucune tâche trouvée. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(dataBasePath);
            
            List<ToDo> tasks = JsonSerializer.Deserialize<List<ToDo>>(json);

            if (tasks == null || !tasks.Any())
            {
                return Ok(new List<ToDo>());
            }
            
            return Ok(tasks);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erreur lors de la récupération des tâches : {e.Message}");
        }
    }
    
    [HttpGet]
    [Route("tasks/{id}")] // Route relative à la route de base "api/task"
    public IActionResult GetTaskById(int id)
    {
        try
        {
            if (!System.IO.File.Exists(dataBasePath))
            {
                return NotFound("Aucune tâche trouvée. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(dataBasePath);
            
            List<ToDo> tasks = JsonSerializer.Deserialize<List<ToDo>>(json);

            if (tasks == null || !tasks.Any())
            {
                return Ok(new List<ToDo>());
            }

            foreach (var task in tasks)
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
    public IActionResult AddTask([FromBody] ToDo task)
    {
        List<ToDo> tasks;

        // Vérification du fichier existant
        if (System.IO.File.Exists(dataBasePath) && new FileInfo(dataBasePath).Length > 0)
        {
            string json = System.IO.File.ReadAllText(dataBasePath, Encoding.UTF8);
            tasks = JsonSerializer.Deserialize<List<ToDo>>(json) ?? new List<ToDo>();
        }
        else
        {
            tasks = new List<ToDo>();
        }

        // Générer un nouvel ID
        int newId = tasks.Any() ? tasks.Max(x => x.Id) + 1 : 1;

        // Ajouter la nouvelle tâche
        ToDo newTodo = new ToDo
        {
            Id = newId,
            Name = task.Name,
        };
        tasks.Add(newTodo);

        // Sérialiser les données en JSON
        string updatedJson = JsonSerializer.Serialize(tasks, options);

        // Écrire le JSON avec encodage explicite UTF-8
        using (var writer = new StreamWriter(dataBasePath, false, Encoding.UTF8))
        {
            writer.Write(updatedJson);
        }

        Console.WriteLine($"Tâche ajoutée avec succès : Id={newTodo.Id}, Name={newTodo.Name}");
        return Ok($"Tâche ajoutée avec succès : Id={newTodo.Id}, Name={newTodo.Name}");
    }
    
    [HttpDelete]
    [Route("tasks/{id}")] // Route relative à la route de base "api/task"
    public IActionResult DeleteTask(int id)
    {
        List<ToDo> tasks ;
        
        // Récupérer les données
        if (System.IO.File.Exists(dataBasePath) && new FileInfo(dataBasePath).Length > 0)
        {
            string json = System.IO.File.ReadAllText(dataBasePath);
            tasks = JsonSerializer.Deserialize<List<ToDo>>(json) ?? new List<ToDo>();
            
            foreach (var task in tasks.ToList())
            {
                if (task.Id == id)
                {
                    tasks.Remove(task);
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(tasks, options);
            System.IO.File.WriteAllText(dataBasePath, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(204);
    }
    
    [HttpPut]
    [Route("tasks/{id}")] // Route relative à la route de base "api/task"
    public IActionResult UpdateTask(int id, ToDo toDo)
    {
        List<ToDo> tasks ;
        
        // Récupérer les données
        if (System.IO.File.Exists(dataBasePath) && new FileInfo(dataBasePath).Length > 0)
        {
            string json = System.IO.File.ReadAllText(dataBasePath);
            tasks = JsonSerializer.Deserialize<List<ToDo>>(json) ?? new List<ToDo>();
            
            foreach (var task in tasks.ToList())
            {
                if (task.Id == id)
                {
                    task.Name = toDo.Name;
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(tasks, options);
            System.IO.File.WriteAllText(dataBasePath, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(204);
    }
}