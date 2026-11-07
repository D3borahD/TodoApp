using Domain.Enums;

namespace Domain.Entities;

public class ProjectTypeDao
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public ProjectTypeColor ProjectTypeColor { get; set; }
    
}