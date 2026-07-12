using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ReferentialService(ILogger<ReferentialService> logger, IBaseRepository<TypeDao> projectTypeRepository)
    : IReferentialService
{
    public Task<List<Status>> GetStatusAsync()
    {
        var statuses = Enum.GetValues<Status>().ToList();
        return Task.FromResult(statuses);
    }
    
    public async Task<List<TypeDao>> GetProjectTypeAsync()
    {
       return await projectTypeRepository.GetAllAsync();
    }
}