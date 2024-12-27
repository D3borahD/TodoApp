using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

public class ToDoTask
{
    public int Id { get; set; }
    public string Name { get; set; }
}

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly string dataBasePath = "/Users/deborah/Documents/dev/TodoApp/Backend/datas";

    [HttpGet]
    [Route("tasks")] // Route relative à la route de base "api/task"
    public IActionResult GetTask()
    {
        var task = new
        {
            id = 0,
            name = "Je viens du back"
        };
        return new JsonResult(task);
    }

    [HttpPost]
    [Route("tasks")] // Route relative à la route de base "api/task"
    public IActionResult AddTask([FromBody] ToDoTask task)
    {
        task.Id = 1;
        task.Name = "Je viens du back";

        ToDoTask todo = new ToDoTask
        {
            Id = task.Id,
            Name = task.Name,
        };

        string jsonString = JsonSerializer.Serialize(todo);

        try
        {
            using (StreamWriter fileStream = new StreamWriter(dataBasePath, append: true))
            {
                fileStream.WriteLine($"{jsonString};");
            }
            return Ok(new { message = "Task added successfully" });
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
            return StatusCode(500, "An error occurred while saving the task.");
        }
    }
}