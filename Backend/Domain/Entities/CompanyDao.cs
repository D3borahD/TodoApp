using BackendApi.Entities;

namespace Domain.Entities;

public class CompanyDao :BaseDao
{
    public int LocationId { get; set; }
    public int CountryId { get; set; }
}