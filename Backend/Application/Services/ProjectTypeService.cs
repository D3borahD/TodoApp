using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProjectTypeService(ILogger<ProjectTypeService> logger, IBaseRepository<TypeDao> projectTypeRepository )
    : IProjectTypeService, IBaseService<ProjectTypesDto>
{
       public async Task<List<ProjectTypesDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all Projects");
        var projects = await projectTypeRepository.GetAllAsync();
        
        if (!projects.Any())
        {
            logger.LogInformation("No Projects found");
            return new List<ProjectTypesDto>();
        }
        
        return projects.Select(p => new ProjectTypesDto
        {
            Id = p.Id,
            Label = p.Label
        }).ToList();
    }
    

    public async Task<ProjectTypesDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting Projects by id");
        var projectsDao = await projectTypeRepository.GetByIdAsync(id);
        
        if (projectsDao == null)
        {
            logger.LogInformation("No Projects found");
            return null;
        }
        
        
        return new ProjectTypesDto()
        {
            Id = projectsDao.Id,
            Label = projectsDao.Label,
        };
    }

    public async Task<ProjectTypesDto?> CreateAsync(ProjectTypesDto projectTypeDto)
    {
        logger.LogInformation("Creating new Project");
        
        TypeDao newProjectDao = new TypeDao()
        {
            Label = projectTypeDto.Label.ToLower(),
        };
        
        var createdProject = await projectTypeRepository.CreateAsync(newProjectDao);

        return new ProjectTypesDto()
        {
            Id = createdProject.Id,
            Label = createdProject.Label,
        };
    }

    public async Task<ProjectTypesDto?> UpdateAsync(ProjectTypesDto projectTypeDto)
    {
        var projectDao = await projectTypeRepository.GetByIdAsync(projectTypeDto.Id);
        if(projectDao == null)
        {
            logger.LogInformation("No Projects found");
            return null;
        }
        
        projectDao.Label = projectTypeDto.Label.ToLower();
        
        var updatedProject = await projectTypeRepository.UpdateAsync(projectDao);
        
        return new ProjectTypesDto()
        {
            Id = updatedProject!.Id,
            Label = updatedProject.Label,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var projectDao = await projectTypeRepository.GetByIdAsync(id);
        
        if (projectDao == null)
        {
            logger.LogInformation("No Projects found");
            return false;
        }
        
        await projectTypeRepository.DeleteAsync(projectDao.Id);
        return true;
    }
    
}