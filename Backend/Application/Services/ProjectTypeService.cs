using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProjectTypeService(ILogger<ProjectTypeService> logger, IBaseRepository<ProjectTypeDao> projectTypeRepository )
    : IProjectTypeService, IBaseService<ProjectTypeDto>
{
       public async Task<List<ProjectTypeDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all Projects");
        var projects = await projectTypeRepository.GetAllAsync();
        
        if (!projects.Any())
        {
            logger.LogInformation("No Projects found");
            return new List<ProjectTypeDto>();
        }
        
        return projects.Select(p => new ProjectTypeDto
        {
            Id = p.Id,
            Label = p.Label
        }).ToList();
    }
    

    public async Task<ProjectTypeDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting Projects by id");
        var projectsDao = await projectTypeRepository.GetByIdAsync(id);
        
        if (projectsDao == null)
        {
            logger.LogInformation("No Projects found");
            return null;
        }
        
        
        return new ProjectTypeDto()
        {
            Id = projectsDao.Id,
            Label = projectsDao.Label,
        };
    }

    public async Task<ProjectTypeDto?> CreateAsync(ProjectTypeDto projectTypeDto)
    {
        logger.LogInformation("Creating new Project");
        
        ProjectTypeDao newProjectDao = new ProjectTypeDao()
        {
            Label = projectTypeDto.Label.ToLower(),
        };
        
        var createdProject = await projectTypeRepository.CreateAsync(newProjectDao);

        return new ProjectTypeDto()
        {
            Id = createdProject.Id,
            Label = createdProject.Label,
        };
    }

    public async Task<ProjectTypeDto?> UpdateAsync(ProjectTypeDto projectTypeDto)
    {
        var projectDao = await projectTypeRepository.GetByIdAsync(projectTypeDto.Id);
        if(projectDao == null)
        {
            logger.LogInformation("No Projects found");
            return null;
        }
        
        projectDao.Label = projectTypeDto.Label.ToLower();
        
        var updatedProject = await projectTypeRepository.UpdateAsync(projectDao);
        
        return new ProjectTypeDto()
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