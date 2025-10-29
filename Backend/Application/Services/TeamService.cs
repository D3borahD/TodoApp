using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TeamService(IBaseRepository<TeamDao> teamRepository, ILogger<TeamService> logger)
    : IBaseService<TeamDto>
{
    public async Task<List<TeamDto>> GetAllAsync()
    {
        logger.LogInformation("Getting get all teams.");
        var teamsDao = await teamRepository.GetAllAsync();

        if (!teamsDao.Any())
        {
            logger.LogWarning("⚠No teams found.");
            return new List<TeamDto>();
        }
        
        return teamsDao.Select(t => new TeamDto
        {
            Id = t.Id,
            Label = t.Label
        }).ToList();

    }

    public async Task<TeamDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting teams by id.");
        var teamDao = await teamRepository.GetByIdAsync(id);

        if (teamDao == null)
        {
            logger.LogWarning("No team found"); 
            return null;
        }
        
        return new TeamDto
        {
            Id = teamDao.Id,
            Label = teamDao.Label
        };
    }

    public async Task<TeamDto> CreateAsync(TeamDto teamDto)
    {
        logger.LogInformation("Creating new team.");
        
        TeamDao newTeamDao = new TeamDao
        {
            Label = teamDto.Label.ToLower(),
        };
        
        var createdTeam = await teamRepository.CreateAsync(newTeamDao);

        return new TeamDto
        {
            Id = createdTeam.Id,
            Label = createdTeam.Label
        };
    }

    public async Task<TeamDto?> UpdateAsync(TeamDto teamDto)
    {
        var teamDao = await teamRepository.GetByIdAsync(teamDto.Id);
        if (teamDao == null)
        {
            logger.LogWarning("No team found");
            return null;
        }
        
        teamDao.Label = teamDto.Label.ToLower();
        
        var updatedTeamDao = await teamRepository.UpdateAsync(teamDao);
        if (updatedTeamDao == null) return null;

        return new TeamDto
        {
            Id = updatedTeamDao.Id,
            Label = updatedTeamDao.Label
        };
    }

    public async Task<bool>  DeleteAsync(int id)
    {
        var teamDao = await teamRepository.GetByIdAsync(id);
        
        if (teamDao == null)
        {
            logger.LogWarning("No team found");
            return false;
        }
        
        await teamRepository.DeleteAsync(teamDao.Id);
        return true;
    }
}
