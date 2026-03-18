namespace BackendApi.Entities;

public class BaseDao
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public required string Code  { get; set; }
}