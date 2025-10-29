using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ModuleService(IBaseRepository<ModuleDao> moduleRepository, ILogger<ModuleService> logger) : IBaseService<ModuleDto>
{
    public async Task<List<ModuleDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all modules.");
        var modules = await moduleRepository.GetAllAsync();

        if (!modules.Any())
        {
            logger.LogInformation("No modules found.");
            return new List<ModuleDto>();
        }
        
        return modules.Select(m => new ModuleDto
        {
            Id = m.Id,
            Label =  m.Label,
            ProductId = m.ProductId
        }).ToList();
    }
    
    public async Task<ModuleDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting modules by id.");
        var module = await moduleRepository.GetByIdAsync(id);

        if (module == null) 
        {
            logger.LogInformation("No module found.");
            return null;
        }
        
        return new ModuleDto()
        {
            Id = module.Id,
            Label = module.Label,
            ProductId = module.ProductId
        };
    }

    public async  Task<ModuleDto> CreateAsync(ModuleDto moduleDto)
    {
        logger.LogInformation("Creating new module.");

        ModuleDao moduleDao = new ModuleDao()
        {
            Label = moduleDto.Label.ToLower(),
            ProductId = moduleDto.ProductId
        };
        
        var createdModule = await moduleRepository.CreateAsync(moduleDao);
            
        return new ModuleDto()
        {
            Id = createdModule.Id,
            Label = createdModule.Label,
            ProductId = createdModule.ProductId
        };
    }

    public async Task<ModuleDto?> UpdateAsync(ModuleDto moduleDto)
    {
        var moduleDao = await moduleRepository.GetByIdAsync(moduleDto.Id);
        if (moduleDao == null)
        {
            logger.LogInformation("Module not found.");
            return null;
        }

        moduleDao.Label = moduleDto.Label.ToLower();
        moduleDao.ProductId = moduleDto.ProductId;

        var updatedModule = await moduleRepository.UpdateAsync(moduleDao);
        if (updatedModule == null) return null;

        return new ModuleDto()
        {
            Id = updatedModule.Id,
            Label = updatedModule.Label,
            ProductId = updatedModule.ProductId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var moduleDao = await moduleRepository.GetByIdAsync(id);

        if (moduleDao == null)
        {
            logger.LogWarning("Module with id {Id} not found for deletion.", id);            
            return false;
        }
        
        await moduleRepository.DeleteAsync(moduleDao.Id);
        return true;
    }
}