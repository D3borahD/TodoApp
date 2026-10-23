using Domain.Enums;

namespace Application.DTO;

public class ProjectQueryDto
{
    public string? Label { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Status? Status  { get; set; } 
    public int? ProjectTypeId { get; set; }
}