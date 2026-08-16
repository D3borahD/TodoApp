using Domain.Entities;
using Domain.Enums;

namespace Domain.DTO;

public class StepDto: IEntity
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public string? Description { get; set; }
    public int? Rank { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required ProjectTypesDto Type  { get; set; }
    public int? Duration { get; set; }
    public required Status Status  { get; set; }
    public int ProjectId { get; set; }
}
