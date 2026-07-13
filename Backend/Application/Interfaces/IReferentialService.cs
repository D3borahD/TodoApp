using Domain.Enums;

namespace Application.Interfaces;

public interface IReferentialService
{
    Task<List<Status>> GetStatusAsync();
}