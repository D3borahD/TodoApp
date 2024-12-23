using Microsoft.AspNetCore.Mvc;

namespace BackendApi.controlers;

    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        [Route("/tasks")]
        public IActionResult GetTask()
        {
            var task = new
            {
                id = 0,
                name = "Je viens du back"
            };
            return new JsonResult(task);
        }
    }
    
