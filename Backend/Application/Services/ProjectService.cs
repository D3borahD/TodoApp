using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProjectService(ILogger<ProjectService> logger, IBaseRepository<ProjectDao> projectRepository, IStepRepository stepRepository )
    :  IProjectService
{
    public async Task<List<ProjectDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all Projects");
        var projects = await projectRepository.GetAllAsync();
        
        if (!projects.Any())
        {
            logger.LogInformation("No Projects found");
            return new List<ProjectDto>();
        }

        return projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Label = p.Label,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                //    StepList = p.StepList,
                Status = p.Status,
            }).OrderBy(p => p.Status)
            .ToList();
    }
    
    public async Task<ProjectDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting Projects by id");
        var projectsDao = await projectRepository.GetByIdAsync(id);
        
        if (projectsDao == null)
        {
            logger.LogInformation("No Projects found");
            return null;
        }
        
        var stepList = await stepRepository.GetStepByProjectAsync(projectsDao.Id);
        
        return new ProjectDto()
        {
            Id = projectsDao.Id,
            Label = projectsDao.Label,
            Status = projectsDao.Status,
            StartDate = projectsDao.StartDate,
            StepList = stepList.Select(MapToStepDto).ToList(),
        };
    }

    public async Task<ProjectDto?> CreateAsync(ProjectDto projectDto)
    {
        logger.LogInformation("Creating new Project");
        
        ProjectDao newProjectDao = new ProjectDao()
        {
            Label = projectDto.Label.ToLower(),
            StartDate = projectDto.StartDate,
            EndDate = projectDto.EndDate,
          //  StepList = projectDto.StepList,
            Description = projectDto.Description,
            Status = projectDto.Status,
        };
        
        var createdProject = await projectRepository.CreateAsync(newProjectDao);

        return new ProjectDto()
        {
            Id = createdProject.Id,
            Label = createdProject.Label,
            StartDate = createdProject.StartDate,
            EndDate = createdProject.EndDate,
           // StepList = createdProject.StepList,
            Description = createdProject.Description,
            Status = createdProject.Status,
        };
    }

    public async Task<ProjectDto?> UpdateAsync(ProjectDto projectDto)
    {
        var projectDao = await projectRepository.GetByIdAsync(projectDto.Id);
        if(projectDao == null)
        {
            logger.LogInformation("No Projects found");
            return null;
        }
        
        projectDao.Label = projectDto.Label.ToLower();
        
        var updatedProject = await projectRepository.UpdateAsync(projectDao);
        
        return new ProjectDto()
        {
            Id = updatedProject!.Id,
            Label = updatedProject.Label,
    
            Status = updatedProject.Status,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var projectDao = await projectRepository.GetByIdAsync(id);
        
        if (projectDao == null)
        {
            logger.LogInformation("No Projects found");
            return false;
        }
        
        await projectRepository.DeleteAsync(projectDao.Id);
        return true;
    }
    
    private static StepDto MapToStepDto(StepDao stepDao)
    {
        return new StepDto
        {
            Id = stepDao.Id,
            Label = stepDao.Label,
            Description = stepDao.Description,
            Rank = stepDao.Rank,
            StartDate = stepDao.StartDate,
            EndDate = stepDao.EndDate,
            Status = stepDao.Status,
            Type = new TypeDto()
            {
                Id = stepDao.Type,
                Label = "",
            },
            Duration = stepDao.Duration,
        };
    }
}