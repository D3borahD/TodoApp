using Domain.Enums;

namespace Domain.Entities;

public class TypeDao : IEntity
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public ProjectTypeColor ProjectTypeColor { get; set; }
    public List<ProjectDao> Projects { get; set; } = new List<ProjectDao>();
}