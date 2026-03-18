using BackendApi.Entities;

namespace Domain.Entities;

public class ModuleDao :BaseDao
{
    public int ProductId { get; set; }
    public required DateTime StartDate { get; set; }
    public required string SegmentCode { get; set; }
}