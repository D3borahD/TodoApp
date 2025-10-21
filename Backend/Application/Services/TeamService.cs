using System.Text;
using System.Text.Json;
using Application.Interfaces;
using BackendApi.Models;
using Infrastructure.IRepository;
using Domain.DTO;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly ILogger<TeamService> _logger;

    public TeamService(ITeamRepository teamRepository, ILogger<TeamService> logger)
    {
        _teamRepository = teamRepository;
        _logger = logger;
    }
    
    public async Task<List<TeamDto>> GetTeamsAsync()
    {
        _logger.LogInformation("🔍 Begin get all teams.");
        var teamsDao = await _teamRepository.GetTeamsAsync();

        if (!teamsDao.Any() || teamsDao == null)
        {
            _logger.LogWarning("⚠️ No teams found.");
            return new List<TeamDto>();
        }
        
        var result = teamsDao.Select(t => new TeamDto
        {
            Id = t.Id,
            Label = t.Label
        }).ToList();

        _logger.LogInformation("✅ {Count} teams successfully recovered", result.Count);
        return result;
    }

    public async Task<TeamDto?> GetTeamsByIdAsync(int id)
    {
        _logger.LogInformation("🔍 Team recovery with ID {Id}.", id);
        var teamDao = await _teamRepository.GetTeamByIdAsync(id);

        if (teamDao == null)
        {
            _logger.LogWarning("⚠️ No team found with ID {Id}.",id); 
            return null;
        }
        
        var result = new TeamDto
        {
            Id = teamDao.Id,
            Label = teamDao.Label
        };
            
        _logger.LogInformation("✅ Team {Id} retrieved : {Label}", result.Id, result.Label);
        return result;
    }

    public async Task<TeamDto?> CreateTeamAsync(TeamDto teamDto)
    {
        var teamsDaoList = await _teamRepository.GetTeamsAsync();
        int newId = teamsDaoList.Any() ? teamsDaoList.Max(x => x.Id) + 1 : 1;
        
        TeamDao newTeamDao = new TeamDao
        {
            Id = newId,
            Label = teamDto.Label,
        };
        
        var createdTeam = await _teamRepository.CreateTeamAsync(newTeamDao);
        
        if (createdTeam == null)
            return null;
        
        return new TeamDto
        {
            Id = createdTeam.Id,
            Label = createdTeam.Label
        };
    }
}
