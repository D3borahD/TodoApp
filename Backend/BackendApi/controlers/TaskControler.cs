using System.IO;
using System.Text.Json;
using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackendApi.Controllers;



[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly string dataBasePath = "/Users/deborah/Documents/dev/TodoApp/Backend/datas";

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
        
        if (System.IO.File.Exists(dataBasePath) && new FileInfo(dataBasePath).Length > 0)
        {
           string json = System.IO.File.ReadAllText(dataBasePath);
           tasks = JsonSerializer.Deserialize<List<ToDo>>(json) ?? new List<ToDo>();
        }
        else
        {
            tasks = new List<ToDo>();
        }
        
        int newId = tasks.Any() ? tasks.Max(x => x.Id) + 1 : 0;
        
        ToDo newTodo = new ToDo
        {
            Id = newId,
            Name = task.Name,
        };

        tasks.Add(newTodo);
        
        string updatedJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText(dataBasePath, updatedJson);

        Console.WriteLine($"Tâche ajoutée avec succès : Id={newTodo.Id}, Name={newTodo.Name}");
        return Ok($"Tâche ajoutée avec succès : Id={newTodo.Id}, Name={newTodo.Name}");
    }
}