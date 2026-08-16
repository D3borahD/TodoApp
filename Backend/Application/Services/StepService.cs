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
        
        StepDao newStepDao = new StepDao()
        {
            Description = stepDto.Description,
            Duration = stepDto.Duration,
            EndDate = stepDto.EndDate,
            Label = stepDto.Label,
            ProjectId = stepDto.ProjectId,
            StartDate = stepDto.StartDate,
            Rank = stepDto.Rank,
            Type = stepDto.Type.Id,
            Status = stepDto.Status,
        };
        
        var createdStep = await baseRepository.CreateAsync(newStepDao);
        var type = await projectTypeRepository.GetByIdAsync(stepDto.Type.Id);
        
        return new StepDto()
        {
            Id = createdStep.Id,
            Label = createdStep.Label,
            Description = createdStep.Description,
            Duration = createdStep.Duration,
            EndDate = createdStep.EndDate,
            StartDate = createdStep.StartDate,
            Rank = createdStep.Rank,
            Type = new ProjectTypesDto(){ Id = type.Id, Label = type.Label},
            Status = createdStep.Status,
        };
    }

    public Task<StepDto?> UpdateAsync(StepDto entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<StepDto>> GetStepsByProjectAsync(int projectId)
    {
        logger.LogInformation("Getting step by ProjectId");
        var stepList = await stepRepository.GetStepByProjectAsync(projectId);

        if (!stepList.Any())
        {
            return new List<StepDto>();
        }
        
        var stepDtos = new List<StepDto>();
        
        foreach (var step in stepList)
        {
            var type = await projectTypeRepository.GetByIdAsync(step.Type);

            stepDtos.Add(new StepDto
            {
                Id = step.Id,
                Label = step.Label,
                Description = step.Description,
                Duration = step.Duration,
                EndDate = step.EndDate,
                StartDate = step.StartDate,
                Rank = step.Rank,
                ProjectId = step.ProjectId,
                Type = new ProjectTypesDto
                {
                    Id = type?.Id ?? step.Type,
                    Label = type?.Label ?? string.Empty
                },
                Status = step.Status
            });
        }
        return stepDtos;
    }
}