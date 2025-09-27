namespace BackendApi.Entities;

public class CompanyDao
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public int LocationId { get; set; }
    public int CountryId { get; set; }
}