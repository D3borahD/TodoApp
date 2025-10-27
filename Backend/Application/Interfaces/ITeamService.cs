using Domain.DTO;

namespace Application.Interfaces;

public interface ITeamService
{
    public Task<List<TeamDto>> GetTeamsAsync();
    public Task<TeamDto?> GetTeamsByIdAsync(int id);
    public Task<TeamDto?> CreateTeamAsync(TeamDto teamDto);
    public Task<TeamDto> UpdateTeamAsync(TeamDto teamDto);
    public Task<bool> DeleteTeamAsync(int id);
}