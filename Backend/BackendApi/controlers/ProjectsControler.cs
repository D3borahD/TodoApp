using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;


[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ProjectsControler: ControllerBase
{
    private static readonly string dataBasePath = "/Users/deborah/Documents/dev/TodoApp/Backend/DatasFiles";
    private readonly string projectsdatasPath = $"{dataBasePath}/projectsDatas";
    
    // Options de sérialisation pour désactiver l'encodage des caractères non ASCII
    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    
    [HttpGet]
    [Route("projects")] // Route relative à la route de base "api/task"
    public IActionResult GetProjects()
    {
        try
        {
            if (!System.IO.File.Exists(projectsdatasPath))
            {
                return NotFound("Aucun projets trouvé. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(projectsdatasPath);
            
            List<Projects> projectsList = JsonSerializer.Deserialize<List<Projects>>(json);

            if (projectsList == null || !projectsList.Any())
            {
                return Ok(new List<Projects>());
            }
            
            return Ok(projectsList);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erreur lors de la récupération des équipes : {e.Message}");
        }
    }
    
    [HttpGet]
    [Route("projects/{name}")] // Route relative à la route de base "api/projects"
    public IActionResult GetProjectByName(string name)
    {
        try
        {
            if (!System.IO.File.Exists(projectsdatasPath))
            {
                return NotFound("Aucun projet trouvé. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(projectsdatasPath);
            
            List<Projects> projectsList = JsonSerializer.Deserialize<List<Projects>>(json);

            if (projectsList == null || !projectsList.Any())
            {
                return Ok(new List<Projects>());
            }

            foreach (var project in projectsList)
            {
                if (project.Name == name)
                {
                    return Ok(project);
                }
            }

            return StatusCode(204, $"Ce projet n'existe pas");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erreur lors de la récupération des projets : {e.Message}");
        }
    }
    
    
    [HttpGet]
    [Route("projects/{lot}")] // Route relative à la route de base "api/task"
    public IActionResult GetProjectByLot(string lot)
    {
        try
        {
            if (!System.IO.File.Exists(projectsdatasPath))
            {
                return NotFound("Aucun projet trouvé. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(projectsdatasPath);
            
            List<Projects> projectsList = JsonSerializer.Deserialize<List<Projects>>(json);

            if (projectsList == null || !projectsList.Any())
            {
                return Ok(new List<Projects>());
            }

            foreach (var project in projectsList)
            {
                if (project.Lot == lot)
                {
                    return Ok(project);
                }
            }

            return StatusCode(204, $"Ce projet n'existe pas");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erreur lors de la récupération des projets : {e.Message}");
        }
    }
    
    [HttpGet]
    [Route("projects/{id}")] // Route relative à la route de base "api/task"
    public IActionResult GetProjectById(int id)
    {
        try
        {
            if (!System.IO.File.Exists(projectsdatasPath))
            {
                return NotFound("Aucun projet trouvé. Le fichier de données est manquant.");
            }
            
            string json = System.IO.File.ReadAllText(projectsdatasPath);
            
            List<Projects> projectsList = JsonSerializer.Deserialize<List<Projects>>(json);

            if (projectsList == null || !projectsList.Any())
            {
                return Ok(new List<Projects>());
            }

            foreach (var project in projectsList)
            {
                if (project.Id == id)
                {
                    return Ok(project);
                }
            }

            return StatusCode(204, $"Ce projet n'existe pas");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Erreur lors de la récupération des projets : {e.Message}");
        }
    }

    [HttpPost]
    [Route("projects")] // Route relative à la route de base "api/task"
    public IActionResult AddProject([FromBody] Projects projects)
    {
        List<Projects> projectsList;

        // Vérification du fichier existant
        if (System.IO.File.Exists(projectsdatasPath) && new FileInfo(projectsdatasPath).Length > 0)
        {
            string json = System.IO.File.ReadAllText(projectsdatasPath, Encoding.UTF8);
            projectsList = JsonSerializer.Deserialize<List<Projects>>(json) ?? new List<Projects>();
        }
        else
        {
            projectsList = new List<Projects>();
        }

        // Générer un nouvel ID
        int newId = projectsList.Any() ? projectsList.Max(x => x.Id) + 1 : 1;

        // Ajouter le nouveau projet
        Projects newProject = new Projects
        {
            Id = newId,
            Reference = projects.Reference,
            Name = projects.Name,
            Lot = projects.Lot,
        };
        projectsList.Add(newProject);

        // Sérialiser les données en JSON
        string updatedJson = JsonSerializer.Serialize(projectsList, options);

        // Écrire le JSON avec encodage explicite UTF-8
        using (var writer = new StreamWriter(projectsdatasPath, false, Encoding.UTF8))
        {
            writer.Write(updatedJson);
        }
        
        return StatusCode(201, $"Projet ajouté avec succès : Id={newProject.Id}, Name={newProject.Name}");
    }
    
    [HttpDelete]
    [Route("projects/{id}")] // Route relative à la route de base "api/task"
    public IActionResult DeleteProject(int id)
    {
        List<Projects> projectsList ;
        
        // Récupérer les données
        if (System.IO.File.Exists(projectsdatasPath) && new FileInfo(projectsdatasPath).Length > 0)
        {
            string json = System.IO.File.ReadAllText(projectsdatasPath);
            projectsList = JsonSerializer.Deserialize<List<Projects>>(json) ?? new List<Projects>();
            
            foreach (var task in projectsList.ToList())
            {
                if (task.Id == id)
                {
                    projectsList.Remove(task);
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(projectsList, options);
            System.IO.File.WriteAllText(projectsdatasPath, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(204, "Le projet à bien été supprimé");
    }
    
    [HttpPut]
    [Route("projects/{id}")] // Route relative à la route de base "api/task"
    public IActionResult UpdateProject(int id, Projects projects)
    {
        List<Projects> projectsList ;
        
        // Récupérer les données
        if (System.IO.File.Exists(projectsdatasPath) && new FileInfo(projectsdatasPath).Length > 0)
        {
            string json = System.IO.File.ReadAllText(projectsdatasPath);
            projectsList = JsonSerializer.Deserialize<List<Projects>>(json) ?? new List<Projects>();
            
            foreach (var project in projectsList.ToList())
            {
                if (project.Id == id)
                {
                    project.Name = projects.Name;
                    project.Reference = projects.Reference;
                    project.Lot = projects.Lot;
                    project.Name = projects.Name;
                    continue;
                }
            }

            string updatedJson = JsonSerializer.Serialize(projectsList, options);
            System.IO.File.WriteAllText(projectsdatasPath, updatedJson,  System.Text.Encoding.UTF8);
        }
        
        return StatusCode(200, $"Le projet {id} a été modifié");
    }
    
}