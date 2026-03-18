namespace Domain.Entities;

public class TimeEntryDao
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime WorkDate { get; set; }
    public float Workload { get; set; }
    public int ActivityId { get; set; }
    public int TeamId { get; set; }
    public int ProductId { get; set; }
    public int ModuleId { get; set; }
    public int SpecificProjectId { get; set; }
    public string? Comment { get; set; }
}