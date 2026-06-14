

using Domain.DTO;

namespace Application.Interfaces;

public interface IReferentialService
{
    Task<List<StatusDto>> GetStatusAsync();
}