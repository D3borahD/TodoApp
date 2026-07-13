using Domain.Entities;

namespace Domain.DTO;

public class ProjectTypesDto : IEntity
{
    public int Id { get; set; }
    public required string Label { get; set; }
}