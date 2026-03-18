using BackendApi.Entities;
using Domain.Entities;

namespace Application.Interfaces;

public interface IModuleRepository
{
    public Task<List<ModuleDao>> GetModuleByProductAsync(int productId);
}