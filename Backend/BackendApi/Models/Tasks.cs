namespace BackendApi.Models;

public class Tasks
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TimeSpan EstimatedTime { get; set; }
    public TimeSpan RealisedTime { get; set; }
    public TimeSpan RemainedTime { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ClosedDate { get; set; }
}