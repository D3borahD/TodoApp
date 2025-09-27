namespace BackendApi.Entities;

public class UserTeamHistoryDao
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TeamId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}