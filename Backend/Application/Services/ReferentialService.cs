using Application.Interfaces;
using Domain.DTO;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ReferentialService(ILogger<ReferentialService> logger )
    : IReferentialService
{
    public Task<List<StatusDto>> GetStatusAsync()
    {
        var statuses = Enum.GetValues<Status>()
            .Select(status => new StatusDto
            {
              
                Label = status.ToString(),
            
            })
            .ToList();

        return Task.FromResult(statuses);
    }


}