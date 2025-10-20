namespace BackendApi.Entities;

public class SpecificProjectDao
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}