using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ModuleService(IBaseRepository<ModuleDao> moduleRepository, ILogger<ModuleService> logger) : IModuleService
{
    public async Task<List<ModuleDto>> GetModulesAsync()
    {
        logger.LogInformation("Getting all modules.");
        var modules = await moduleRepository.GetAllAsync();

        if (!modules.Any())
        {
            logger.LogInformation("No modules found.");
            return new List<ModuleDto>();
        }
        
        var result = modules.Select(m => new ModuleDto
        {
            Id = m.Id,
            Label =  m.Label,
            ProductId = m.ProductId
        }).ToList();
        
        logger.LogInformation("Returning modules.");
        return result;
    }

    public async Task<ModuleDto?> GetModulesByIdAsync(int id)
    {
        logger.LogInformation("Getting modules by id.");
        var module = await moduleRepository.GetByIdAsync(id);

        if (module == null) 
        {
            logger.LogInformation("No module found.");
            return null;
        }
        
        var result = new ModuleDto()
        {
            Id = module.Id,
            Label = module.Label,
            ProductId = module.ProductId
        };
        logger.LogInformation("Returning module.");
        return result;
    }

    public async  Task<ModuleDto?> CreateModuleAsync(ModuleDto moduleDto)
    {
        var moduleDaoList = await moduleRepository.GetAllAsync();
        int newId = moduleDaoList.Any() ? moduleDaoList.Max(x => x.Id) + 1 : 1;

        ModuleDao moduleDao = new ModuleDao()
        {
            Id = newId,
            Label = moduleDto.Label.ToLower(),
            ProductId = moduleDto.ProductId
        };
        
        var createdModule = await moduleRepository.CreateAsync(moduleDao);

        logger.LogInformation("Creating new module.");
       return new ModuleDto()
       {
           Id = createdModule.Id,
           Label = createdModule.Label,
           ProductId = createdModule.ProductId
       };
    }

    public async Task<ModuleDto?> UpdateModuleAsync(ModuleDto moduleDto)
    {
        var moduleDaoList = await moduleRepository.GetAllAsync();
        var moduleDao = moduleDaoList.FirstOrDefault(x => x.Id == moduleDto.Id);

        if (moduleDao == null) return null;
       
        moduleDao.Label = moduleDto.Label.ToLower();
        
        var updatedModule = await moduleRepository.UpdateAsync(moduleDao);
        
        if (updatedModule == null) return null;

        return new ModuleDto()
        {
            Id = updatedModule.Id,
            Label = updatedModule.Label,
            ProductId = updatedModule.ProductId
        };
    }

    public async Task<bool> DeleteModuleAsync(int id)
    {
        var moduleDaoList = await moduleRepository.GetAllAsync();
        var moduleDao = moduleDaoList.FirstOrDefault(x => x.Id == id);
        
        if (moduleDao == null) return false;
        
        await moduleRepository.DeleteAsync(moduleDao.Id);
        return true;
    }
}