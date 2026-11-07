using Domain.Entities;

namespace Application.DTO;

public static class ProjectMappingExtension
{
    public static StepResponseDto MapToStepDto(StepDao stepDao)
    {
        return new StepResponseDto
        {
            Id = stepDao.Id,
            Label = stepDao.Label,
            Description = stepDao.Description,
            Rank = stepDao.Rank,
            StartDate = stepDao.StartDate,
            EndDate = stepDao.EndDate,
            Status = stepDao.Status,
            Duration = stepDao.Duration,
        };
    }
    
    /*public static ProjectTypesDto MapToProjectTypesDto(TypeDao typeDao)
    {
        return new ProjectTypesDto()
        {
            Id = typeDao.Id,
            Label = typeDao.Label,
        };
    }*/
}