using Domain.DTO;

namespace Application.Interfaces;

public interface ITeamService
{
    public Task<List<TeamDto>> GetTeamsAsync();
}