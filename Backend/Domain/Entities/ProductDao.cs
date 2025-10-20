namespace BackendApi.Entities;

public class ProductDao
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public int BusinessUnitId { get; set; }
}