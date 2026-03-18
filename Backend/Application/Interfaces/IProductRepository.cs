using Domain.Entities;

namespace Application.Interfaces;

public interface IProductRepository
{
    public Task<List<ProductDao>> GetProductByTeamAsync(int teamId);
}