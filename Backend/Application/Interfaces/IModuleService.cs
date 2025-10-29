using Domain.DTO;

namespace Application.Interfaces;

public interface IModuleService
{
    public Task<List<ModuleDto>> GetModulesAsync();
    public Task<ModuleDto?> GetModulesByIdAsync(int id);
    public Task<ModuleDto?> CreateModuleAsync(ModuleDto moduleDto);
    public Task<ModuleDto?> UpdateModuleAsync(ModuleDto moduleDto);
    public Task<bool> DeleteModuleAsync(int id);
}