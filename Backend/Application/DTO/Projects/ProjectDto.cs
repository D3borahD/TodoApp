using Domain.Enums;

namespace Application.DTO;

public class ProjectResponseDto
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<StepResponseDto> StepList { get; set; } = new List<StepResponseDto>();
    public required Status Status  { get; set; } 
    
    public List<ProjectTypesDto> ProjectTypes { get; set; } = new List<ProjectTypesDto>();
}