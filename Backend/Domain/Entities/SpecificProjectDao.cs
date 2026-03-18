using BackendApi.Entities;

namespace Domain.Entities;

public class SpecificProjectDao: BaseDao
{

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}