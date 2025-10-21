using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using BackendApi.Helpers;
using BackendApi.Models;
using Domain.DTO;
using Infrastructure.IRepository;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository;

public class TeamRepository : ITeamRepository
{
    private readonly string _path = FilePathHelper.GetPath("teamsDatas");
    private readonly ILogger<TeamRepository> _logger;

    public TeamRepository(ILogger<TeamRepository> logger)
    {
        _logger = logger;
    }
    
    public static readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public async Task<List<TeamDao>> GetTeamsAsync()
    {
        try
        {
            if (!File.Exists(_path))
                _logger.LogInformation("No team found");

            string json = await File.ReadAllTextAsync(_path);

            List<TeamDao> teamsList = JsonSerializer.Deserialize<List<TeamDao>>(json);

            return teamsList ?? new List<TeamDao>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during team search");
            return new List<TeamDao>();
        }
    }

    public async Task<TeamDao?> GetTeamByIdAsync(int id)
    {
        try
        {
            var teams = await GetTeamsAsync();
            var team = teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
                _logger.LogInformation("No team found for id {id}", id);
            return team;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while searching for the team  {Id}", id);
            return null;
        }
    }

    public async Task<TeamDao> CreateTeamAsync(TeamDao teamDto)
    {
        try
        {
            var teamsList = await GetTeamsAsync();
            teamsList.Add(teamDto);

            // Sérialiser les données en JSON
            string updatedJson = JsonSerializer.Serialize(teamsList, options);
            // Écrire le JSON avec encodage explicite UTF-8
            await File.WriteAllTextAsync(_path, updatedJson, Encoding.UTF8);

            _logger.LogInformation("✅ New team created: {Label} (ID {Id})", teamDto.Label, teamDto.Id);
       
            return teamDto;

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error during team creation");
            return null;
        }
            
    }
}

