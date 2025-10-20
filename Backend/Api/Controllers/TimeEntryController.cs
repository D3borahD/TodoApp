using System.Text.Encodings.Web;
using System.Text.Json;
using BackendApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;


[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class TimeEntryController
{
    private string path = FilePathHelper.GetPath("tasksDatas");
    
    // Options de sérialisation pour désactiver l'encodage des caractères non ASCII
    public static readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

}