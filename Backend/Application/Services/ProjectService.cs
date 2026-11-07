using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProjectService(ILogger<ProjectService> logger, IBaseRepository<ProjectDao> baseRepository, 
    IProjectRepository projectRepository, IStepRepository stepRepository, IBaseRepository<ProjectTypeDao> projectTypeRepository)
    :  IProjectService
{
    public async Task<List<ProjectResponseDto>> GetAllAsync(ProjectQueryDto projectQueryDto)
    {
        logger.LogInformation("Getting all Projects");
        var projects = await projectRepository.SearchAsync(projectQueryDto);
        
        if (!projects.Any())
        {
            logger.LogInformation("No Projects found");
            return new List<ProjectResponseDto>();
        }
        
        var projectTypeLabelsById = new Dictionary<int, string>();
        foreach (var projectTypeId in projects.Select(p => p.ProjectTypeId).Distinct())
        {
            projectTypeLabelsById[projectTypeId] = await GetProjectTypesAsync(projectTypeId);
        }

        var project= projects.Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Label = p.Label,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status,
                StepList = p.StepList.Select(ProjectMappingExtension.MapToStepDto).ToList(),
                ProjectTypeLabel = p.ProjectType.Label
            })
            .ToList();
        
        return project;
    }

  
    

    public async Task<ProjectResponseDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting Projects by id");
        var projectsDao = await baseRepository.GetByIdAsync(id);
        
        if (projectsDao == null)
        {
            logger.LogInformation("No Projects found");
            return null;
        }
        
        var stepList = await stepRepository.GetStepByProjectAsync(projectsDao.Id);
        
        return new ProjectResponseDto()
        {
            Id = projectsDao.Id,
            Label = projectsDao.Label,
            Status = projectsDao.Status,
            StartDate = projectsDao.StartDate,
            Description = projectsDao.Description,
            //ProjectTypes = GetAllProjectTypes(projectsDao),
            StepList = stepList.Select(ProjectMappingExtension.MapToStepDto).ToList(),
        };
    }

    public async Task<ProjectResponseDto?> CreateAsync(ProjectRequestDto projectDto)
    {
        logger.LogInformation("Creating new Project");
        
        ProjectDao newProjectDao = new ProjectDao()
        {
            Label = projectDto.Label.ToLower(),
            StartDate = projectDto.StartDate,
            EndDate = projectDto.EndDate,
            Description = projectDto.Description,
            Status = projectDto.Status,
            ProjectType = new ProjectTypeDao(){Label = ""}
        };
        
        var createdProject = await baseRepository.CreateAsync(newProjectDao);

        return new ProjectResponseDto()
        {
            Id = createdProject.Id,
            Label = createdProject.Label,
            StartDate = createdProject.StartDate,
            EndDate = createdProject.EndDate,
            Description = createdProject.Description,
            Status = createdProject.Status,
        };
    }

    public async Task<ProjectResponseDto?> UpdateAsync(ProjectRequestDto projectDto)
    {
        var projectDao = await baseRepository.GetByIdAsync(projectDto.Id);
        if(projectDao == null)
        {
            logger.LogInformation("No Projects found");
            return null;
        }
        
        projectDao.Label = projectDto.Label.ToLower();
        
        var updatedProject = await baseRepository.UpdateAsync(projectDao);
        
        return new ProjectResponseDto()
        {
            Id = updatedProject!.Id,
            Label = updatedProject.Label,
            StartDate = updatedProject.StartDate,
            Status = updatedProject.Status,
            EndDate = updatedProject.EndDate,
            Description = updatedProject.Description,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var projectDao = await baseRepository.GetByIdAsync(id);
        
        if (projectDao == null)
        {
            logger.LogInformation("No Projects found");
            return false;
        }
        
        await baseRepository.DeleteAsync(projectDao.Id);
        return true;
    }
    
    
    private async Task<string> GetProjectTypesAsync(int projectTypeId)
    {
        var projectType = await projectTypeRepository.GetByIdAsync(projectTypeId);
        return projectType!.Label;
    }
}