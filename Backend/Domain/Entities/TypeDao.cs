namespace Domain.Entities;

public class TypeDao : IEntity
{
    public int Id { get; set; }
    public required string Label { get; set; }
}