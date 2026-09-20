using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class StepService(ILogger<StepService> logger, IBaseRepository<StepDao> baseRepository, IStepRepository stepRepository, IBaseRepository<TypeDao> projectTypeRepository) 
    : IStepService
{
    public Task<List<StepDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<StepDto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<StepDto?> CreateAsync(StepDto stepDto)
    {
        logger.LogInformation("Creating new Step");
        var type = await projectTypeRepository.GetByIdAsync(stepDto.Type.Id);
        if (type is null)
        {
            logger.LogWarning("Type {TypeId} not found, step not created", stepDto.Type.Id);
            return null;
        }
        
        var newStepDao = new StepDao()
        {
            Label = stepDto.Label,
            Description = stepDto.Description,
            Duration = stepDto.Duration,
            EndDate = stepDto.EndDate,
            Rank = stepDto.Rank,
            ProjectId = stepDto.ProjectId,
            StartDate = stepDto.StartDate,
            TypeId = type.Id,
            Status = stepDto.Status,
        };
        
        var createdStep = await baseRepository.CreateAsync(newStepDao);
 
        
        return new StepDto()
        {
            Id = createdStep.Id,
            Label = createdStep.Label,
            Description = createdStep.Description,
            Duration = createdStep.Duration,
            EndDate = createdStep.EndDate,
            StartDate = createdStep.StartDate,
            Rank = createdStep.Rank,
            Type = new ProjectTypesDto(){ Id = type!.Id, Label = type.Label},
            Status = createdStep.Status,
        };
    }

    public async Task<StepDto?> UpdateAsync(StepDto stepDto)
    {
        logger.LogInformation("update Step {id}", stepDto.Id);
        var stepDao =  await stepRepository.GetByIdAsync(stepDto.Id);
        
        if (stepDao == null)
        {
            logger.LogInformation("No Step found");
            return null;
        }
        
        
  
        stepDao.Label = stepDto.Label;
        stepDao.Description = stepDto.Description;
        stepDao.Duration = stepDto.Duration;
        stepDao.StartDate = stepDto.StartDate;
        stepDao.EndDate = stepDto.EndDate;
        stepDao.Rank = stepDto.Rank;
        stepDao.TypeId = stepDto.Type.Id;
        stepDao.Status = stepDto.Status;

            
          
        var updateStep =  await stepRepository.UpdateAsync(stepDao);
        
        var type = await projectTypeRepository.GetByIdAsync(stepDto.Type.Id);
        
        return MapToDto(stepDao, type);
        
        
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var stepDao = await baseRepository.GetByIdAsync(id);
        
        if (stepDao == null)
        {
            logger.LogInformation("No Step found");
            return false;
        }
        
        await baseRepository.DeleteAsync(stepDao.Id);
        return true;
    }

    public async Task<List<StepDto>> GetStepsByProjectAsync(int projectId)
    {
        logger.LogInformation("Getting steps for project {ProjectId}", projectId);

        var steps = await stepRepository.GetStepByProjectAsync(projectId);
        var stepDtos = new List<StepDto>();

        foreach (var step in steps)
        {
            var type = await projectTypeRepository.GetByIdAsync(step.TypeId);
            stepDtos.Add(MapToDto(step, type));
        }

        return stepDtos;
    }
    
    private static StepDto MapToDto(StepDao step, TypeDao? type)
    {
        return new StepDto
        {
            Id = step.Id,
            Label = step.Label,
            Description = step.Description,
            Duration = step.Duration,
            StartDate = step.StartDate,
            EndDate = step.EndDate,
            Rank = step.Rank,
            ProjectId = step.ProjectId,
            Type = new ProjectTypesDto
            {
                Id = step.TypeId,
                Label = type?.Label ?? string.Empty
            },
            Status = step.Status
        };
    }
}