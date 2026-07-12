using Domain.Entities;
using Domain.Enums;

namespace Domain.DTO;

public class ProjectDto : IEntity
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<StepDto> StepList { get; set; } = new List<StepDto>();
    public required Status Status  { get; set; } 
}