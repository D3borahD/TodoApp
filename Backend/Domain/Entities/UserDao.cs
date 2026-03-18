namespace Domain.Entities;

public class UserDao
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string FullName { get; set; }
    public required string Role { get; set; }
    public int? CompanyId { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime DepartureDate { get; set; }
}