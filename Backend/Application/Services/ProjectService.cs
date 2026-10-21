using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProjectService(ILogger<ProjectService> logger, IBaseRepository<ProjectDao> projectRepository, IStepRepository stepRepository )
    :  IProjectService
{
    public async Task<List<ProjectResponseDto?>> GetAllAsync()
    {
        logger.LogInformation("Getting all Projects");
        var projects = await projectRepository.GetAllAsync();
        
        if (!projects.Any())
        {
            logger.LogInformation("No Projects found");
            return new List<ProjectResponseDto>();
        }

        return projects.Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Label = p.Label,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status,
                StepList = p.StepList.Select(ProjectMappingExtension.MapToStepDto).ToList(),
                ProjectTypes = GetAllProjectTypes(p),
            }).OrderBy(p => p.Status)
            .ToList();
    }

    private List<ProjectTypesDto> GetAllProjectTypes(ProjectDao projectDao)
    {
        return projectDao.StepList
            .Select(step => step.Type)
            .DistinctBy(type => type.Id)
            .Select(ProjectMappingExtension.MapToProjectTypesDto)
            .ToList();
    }
    

    public async Task<ProjectResponseDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting Projects by id");
        var projectsDao = await projectRepository.GetByIdAsync(id);
        
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
            ProjectTypes = GetAllProjectTypes(projectsDao),
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
        };
        
        var createdProject = await projectRepository.CreateAsync(newProjectDao);

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
        var projectDao = await projectRepository.GetByIdAsync(projectDto.Id);
        if(projectDao == null)
        {
            logger.LogInformation("No Projects found");
            return null;
        }
        
        projectDao.Label = projectDto.Label.ToLower();
        
        var updatedProject = await projectRepository.UpdateAsync(projectDao);
        
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
        var projectDao = await projectRepository.GetByIdAsync(id);
        
        if (projectDao == null)
        {
            logger.LogInformation("No Projects found");
            return false;
        }
        
        await projectRepository.DeleteAsync(projectDao.Id);
        return true;
    }
}