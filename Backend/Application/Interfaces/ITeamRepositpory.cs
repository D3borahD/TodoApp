using BackendApi.Models;
using Domain.DTO;

namespace Infrastructure.IRepository;

public interface ITeamRepository
{
    public Task<List<TeamDao>> GetTeamsAsync();
    public Task<TeamDao?> GetTeamByIdAsync(int id);
    public Task<TeamDao> CreateTeamAsync(TeamDao teamDto);
}