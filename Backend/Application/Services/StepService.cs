using Application.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class StepService(ILogger<StepService> logger, IBaseRepository<StepDao> stepRepository) 
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
        
        var createdStep = await stepRepository.CreateAsync(newStepDao);
        
        

        return new StepDto()
        {
            Id = createdStep.Id,
            Label = createdStep.Label,
            Description = createdStep.Description,
            Duration = createdStep.Duration,
            EndDate = createdStep.EndDate,
            StartDate = createdStep.StartDate,
            Rank = createdStep.Rank,
            Type = new ProjectTypesDto(){Id = createdStep.Type, Label = ""},
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
}