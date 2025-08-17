namespace BackendApi.Models;

public class TimeEntryDAO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TeamId { get; set; }
    public DateTime WorkDate { get; set; }
    public int Minutes { get; set; }
    public int ActivityId { get; set; }
    public int ProductId { get; set; }
    public int ModuleId { get; set; }
    public int SpecificProjectId { get; set; }
    public string Comment { get; set; }
}