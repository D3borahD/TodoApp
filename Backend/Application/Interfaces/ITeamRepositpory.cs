using BackendApi.Models;

namespace Infrastructure.IRepository;

public interface ITeamRepository
{
    public Task<List<TeamDao>> GetTeamsAsync();
}