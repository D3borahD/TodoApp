

using Domain.Entities;

namespace Application.DTO;

public static class StepMappingExtensions
{
    public static StepResponseDto MapToDto(StepDao step, TypeDao? type)
    {
        return new StepResponseDto
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