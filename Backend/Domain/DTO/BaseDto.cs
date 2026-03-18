namespace Domain.DTO;

public class BaseDto
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public string? Code  { get; set; }
}