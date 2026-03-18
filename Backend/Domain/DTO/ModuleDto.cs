using System.Text.Json.Serialization;

namespace Domain.DTO;

public class ModuleDto : BaseDto
{
   // public List<ProductDto?>? Products { get; set; } = new ();
   public string? SegmentCode { get; set; }
   public DateTime? StartDate { get; set; }
}