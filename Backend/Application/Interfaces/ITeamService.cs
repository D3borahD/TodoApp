using Domain.DTO;

namespace Application.Interfaces;

public interface ITeamService : IBaseService<TeamDto>
{
    public Task<List<ProductDto>> GetProductByTeamAsync(int teamId);

}