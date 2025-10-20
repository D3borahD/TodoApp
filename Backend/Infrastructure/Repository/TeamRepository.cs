using System.Text.Json;
using BackendApi.Helpers;
using BackendApi.Models;
using Infrastructure.IRepository;

namespace Infrastructure.Repository;

public class TeamRepository : ITeamRepository
{
    private readonly string _path = FilePathHelper.GetPath("teamsDatas");
    
    public async Task<List<TeamDao>> GetTeamsAsync()
    {
        try
        {
            if (!File.Exists(_path))
            {
                return new List<TeamDao>();
            }

            string json = await File.ReadAllTextAsync(_path);

            List<TeamDao> teamsList = JsonSerializer.Deserialize<List<TeamDao>>(json);

            return teamsList ?? new List<TeamDao>();
        }
        catch (Exception e)
        {
            return new List<TeamDao>();
        }
    }
}

