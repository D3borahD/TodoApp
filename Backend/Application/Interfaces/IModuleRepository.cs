using BackendApi.Entities;

namespace Application.Interfaces;

public interface IModuleRepository
{
    public Task<List<ModuleDao>> GetModulesAsync();
    public Task<ModuleDao?> GetModuleByIdAsync(int id);
    public Task<ModuleDao> CreateModuleAsync(ModuleDao ModuleDao);
    public Task<ModuleDao?> UpdateModuleAsync(ModuleDao ModuleDao);
    public Task DeleteModuleAsync(int id);
}