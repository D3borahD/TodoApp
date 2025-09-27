namespace BackendApi.Entities;

public class ModuleDao
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public int ProductId { get; set; }
}