using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class StepService(
    ILogger<StepService> logger, 
    IStepRepository stepRepository, 
    IBaseRepository<TypeDao> projectTypeRepository) 
    : IStepService
{
    

    public async Task<StepResponseDto?> CreateAsync(StepRequestDto stepResquestDto)
    {
        logger.LogInformation("Creating new Step");
        var type = await projectTypeRepository.GetByIdAsync(stepResquestDto.TypeId);
        if (type is null)
        {
            logger.LogWarning("Type {TypeId} not found, step not created", stepResquestDto.TypeId);
            return null;
        }
        
        var newStepDao = new StepDao()
        {
            Label = stepResquestDto.Label,
            Description = stepResquestDto.Description,
            Duration = stepResquestDto.Duration,
            EndDate = stepResquestDto.EndDate,
            Rank = stepResquestDto.Rank,
            ProjectId = stepResquestDto.ProjectId,
            StartDate = stepResquestDto.StartDate,
            TypeId = type.Id,
            Status = stepResquestDto.Status,
        };
        
        var createdStep = await stepRepository.CreateAsync(newStepDao);
 
        
        return new StepResponseDto()
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

    public async Task<StepResponseDto?> UpdateAsync(StepResponseDto stepResponseDto)
    {
        logger.LogInformation("update Step {id}", stepResponseDto.Id);
        var stepDao =  await stepRepository.GetByIdAsync(stepResponseDto.Id);
        
        if (stepDao == null)
        {
            logger.LogInformation("No Step found");
            return null;
        }
        
        stepDao.Label = stepResponseDto.Label;
        stepDao.Description = stepResponseDto.Description;
        stepDao.Duration = stepResponseDto.Duration;
        stepDao.StartDate = stepResponseDto.StartDate;
        stepDao.EndDate = stepResponseDto.EndDate;
        stepDao.Rank = stepResponseDto.Rank;
        stepDao.TypeId = stepResponseDto.Type.Id;
        stepDao.Status = stepResponseDto.Status;
        
       await stepRepository.UpdateAsync(stepDao);
        
        var type = await projectTypeRepository.GetByIdAsync(stepResponseDto.Type.Id);
        
        return StepMappingExtensions.MapToDto(stepDao, type);
        
        
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var stepDao = await stepRepository.GetByIdAsync(id);
        
        if (stepDao == null)
        {
            logger.LogInformation("No Step found");
            return false;
        }
        
        await stepRepository.DeleteAsync(stepDao.Id);
        return true;
    }

    public async Task<List<StepResponseDto>> GetStepsByProjectAsync(int projectId)
    {
        logger.LogInformation("Getting steps for project {ProjectId}", projectId);

        var steps = await stepRepository.GetStepByProjectAsync(projectId);
        var stepDtos = new List<StepResponseDto>();

        foreach (var step in steps)
        {
            var type = await projectTypeRepository.GetByIdAsync(step.TypeId);
            stepDtos.Add(StepMappingExtensions.MapToDto(step, type));
        }

        return stepDtos;
    }
    
   
}