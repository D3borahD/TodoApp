using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces;

public interface IReferentialService
{
    Task<List<Status>> GetStatusAsync();
    
    Task<List<TypeDao>> GetProjectTypeAsync();
}