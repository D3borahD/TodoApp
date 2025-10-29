using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TeamService(IBaseRepository<TeamDao> teamRepository, ILogger<TeamService> logger)
    : ITeamService
{
    public async Task<List<TeamDto>> GetTeamsAsync()
    {
        logger.LogInformation("🔍 Begin get all teams.");
        var teamsDao = await teamRepository.GetAllAsync();

        if (!teamsDao.Any() || teamsDao == null)
        {
            logger.LogWarning("⚠️ No teams found.");
            return new List<TeamDto>();
        }
        
        var result = teamsDao.Select(t => new TeamDto
        {
            Id = t.Id,
            Label = t.Label
        }).ToList();

        logger.LogInformation("✅ {Count} teams successfully recovered", result.Count);
        return result;
    }

    public async Task<TeamDto?> GetTeamsByIdAsync(int id)
    {
        logger.LogInformation("🔍 Team recovery with ID {Id}.", id);
        var teamDao = await teamRepository.GetByIdAsync(id);

        if (teamDao == null)
        {
            logger.LogWarning("⚠️ No team found with ID {Id}.",id); 
            return null;
        }
        
        var result = new TeamDto
        {
            Id = teamDao.Id,
            Label = teamDao.Label
        };
            
        logger.LogInformation("✅ Team {Id} retrieved : {Label}", result.Id, result.Label);
        return result;
    }

    public async Task<TeamDto?> CreateTeamAsync(TeamDto teamDto)
    {
        var teamsDaoList = await teamRepository.GetAllAsync();
        int newId = teamsDaoList.Any() ? teamsDaoList.Max(x => x.Id) + 1 : 1;
        
        TeamDao newTeamDao = new TeamDao
        {
            Id = newId,
            Label = teamDto.Label.ToLower(),
        };
        
        var createdTeam = await teamRepository.CreateAsync(newTeamDao);
        
        if (createdTeam == null)
            return null;
        
        return new TeamDto
        {
            Id = createdTeam.Id,
            Label = createdTeam.Label
        };
    }

    public async Task<TeamDto> UpdateTeamAsync(TeamDto teamDto)
    {
        var teamDaoList = await teamRepository.GetAllAsync();
        var existingTeam = teamDaoList.FirstOrDefault(t => t.Id == teamDto.Id);
        
        if (existingTeam == null)
            return null;
        
        existingTeam.Label = teamDto.Label.ToLower();
        
        var updatedTeamDao = await teamRepository.UpdateAsync(existingTeam);
        
        if (updatedTeamDao == null)
            return null;

        return new TeamDto
        {
            Id = updatedTeamDao.Id,
            Label = updatedTeamDao.Label
        };
    }

    public async Task<bool>  DeleteTeamAsync(int id)
    {
        var teamDaoList = await teamRepository.GetAllAsync();
        var existingTeam = teamDaoList.FirstOrDefault(t => t.Id == id);
        
        if (existingTeam == null)
            return false; 
        
        await teamRepository.DeleteAsync(existingTeam.Id);
        return true;
    }
}
