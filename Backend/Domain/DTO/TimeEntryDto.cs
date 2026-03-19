namespace Domain.DTO;

public class TimeEntryDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime WorkDate { get; set; }
    public float Workload { get; set; }

    public ActivityDto? Activity { get; set; }
    public TeamDto? Team { get; set; }
    public ProductDto? Product { get; set; }

    public ModuleSummaryDto? Module { get; set; }

    public int? SpecificProjectId { get; set; }
    public string? Comment { get; set; }
}