using BackendApi.Entities;

namespace Application.Interfaces;

public interface IModuleRepository
{
    public Task<List<ModuleDao>> GetModuleByProductAsync(int productId);
}