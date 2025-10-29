namespace Domain.DTO;

public class ModuleDto
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public required int ProductId { get; set; }
}