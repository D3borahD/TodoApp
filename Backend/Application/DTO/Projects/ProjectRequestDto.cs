using Domain.Enums;

namespace Application.DTO;

public class ProjectRequestDto
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required Status Status  { get; set; } 
}