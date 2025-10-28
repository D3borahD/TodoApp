namespace Domain.DTO;

public class ProductDto
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public required int BusinessUnitId { get; set; }
}