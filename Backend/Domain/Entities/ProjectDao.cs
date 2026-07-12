using Domain.Enums;

namespace Domain.Entities;

public class ProjectDao : IEntity
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<StepDao> StepList { get; set; } = new List<StepDao>();  

    public required Status Status { get; set; } 
}

