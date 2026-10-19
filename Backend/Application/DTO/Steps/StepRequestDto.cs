using Domain.Enums;

namespace Application.DTO;

public class StepRequestDto 
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public string? Description { get; set; }
    public int? Rank { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required int TypeId  { get; set; }
    public int? Duration { get; set; }
    public required Status Status  { get; set; }
    public int ProjectId { get; set; }
}
