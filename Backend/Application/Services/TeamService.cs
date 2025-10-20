using Application.Interfaces;
using Infrastructure.IRepository;
using Domain.DTO;

namespace Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;

    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    
    public async Task<List<TeamDto>> GetTeamsAsync()
    {
        var teamsDao = await _teamRepository.GetTeamsAsync();
        
        // mapping DAO -> DTO
        var result = teamsDao.Select(t => new TeamDto
        {
            Id = t.Id,
            Label = t.Label
        }).ToList();

        return result;
    }
}
