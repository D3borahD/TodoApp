using Application.Interfaces;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ReferentialService(ILogger<ReferentialService> logger)
    : IReferentialService
{
    public Task<List<Status>> GetStatusAsync()
    {
        var statuses = Enum.GetValues<Status>().ToList();
        return Task.FromResult(statuses);
    }
    
}