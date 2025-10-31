using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TimeEntryService(
    ITimeEntryRepository timeEntryRepository, 
    ILogger<TimeEntryService> logger, 
    IBaseRepository<ActivityDao> activityRepository, 
    IBaseRepository<TeamDao> teamRepository, 
    IBaseRepository<ProductDao> productRepository, 
    IBaseRepository<ModuleDao> moduleRepository): ITimeEntryService
{
    public async Task<List<TimeEntryDto>> GetAllAsync()
    {
        logger.LogInformation("Getting get all time entries.");
        var timeEntries = await timeEntryRepository.GetAllAsync();

        if (!timeEntries.Any())
        {
            logger.LogWarning("No teams found.");
            return new List<TimeEntryDto>();
        }
        
        var activities = await activityRepository.GetAllAsync();
        var teams = await teamRepository.GetAllAsync();
        var products = await productRepository.GetAllAsync();
        var modules = await moduleRepository.GetAllAsync();
        
        var result = timeEntries.Select(t =>
        {
            var activity = activities.FirstOrDefault(a => a.Id == t.ActivityId);
            var team = teams.FirstOrDefault(x => x.Id == t.TeamId);
            var product = products.FirstOrDefault(p => p.Id == t.ProductId);
            var module = modules.FirstOrDefault(m => m.Id == t.ModuleId);

            return new TimeEntryDto
            {
                Id = t.Id,
                UserId = t.UserId,
                WorkDate = t.WorkDate,
                Workload = t.Workload,
                SpecificProjectId = t.SpecificProjectId,
                Comment = t.Comment,
                Activity = activity != null ? new ActivityDto { Id = activity.Id, Label = activity.Label } : null,
                Team = team != null ? new TeamDto { Id = team.Id, Label = team.Label } : null,
                Product = product != null ? new ProductDto
                    {
                        Id = product.Id,
                        Label = product.Label,
                        BusinessUnitId = 0
                    }
                    : null,
                Module = module != null ? new ModuleSummaryDto()
                {
                    Id = module.Id, 
                    Label = module.Label
                } : null
            };
        }).ToList();
        
        return result;
    }

 


    public async Task<TimeEntryDto> CreateAsync(TimeEntryCreateDto entity)
    {
        logger.LogInformation("Creating new team.");

    
            TimeEntryDao newTimeEntry = new TimeEntryDao()
            {
                UserId = entity.UserId,
                WorkDate = entity.WorkDate,
                Workload = entity.Workload,
                ActivityId = entity.ActivityId,
                ProductId = entity.ProductId,
                TeamId = entity.TeamId,
                ModuleId = entity.ModuleId,
                SpecificProjectId = entity.SpecificProjectId,
                Comment = entity.Comment
            };
        
            var createdTimeEntry = await timeEntryRepository.CreateAsync(newTimeEntry);
            
            var activity = await activityRepository.GetByIdAsync(createdTimeEntry.ActivityId);
            var team = await teamRepository.GetByIdAsync(createdTimeEntry.TeamId);
            var module = await moduleRepository.GetByIdAsync(createdTimeEntry.ModuleId);
            var product = await productRepository.GetByIdAsync(createdTimeEntry.ProductId);

            return new TimeEntryDto()
            {
                Id = createdTimeEntry.Id,
                UserId = createdTimeEntry.UserId,
                WorkDate = createdTimeEntry.WorkDate,
                Workload = createdTimeEntry.Workload,
                SpecificProjectId = createdTimeEntry.SpecificProjectId,
                Comment = createdTimeEntry.Comment,
                Activity = activity != null ? new ActivityDto { Id = activity.Id, Label = activity.Label } : null,
                Product = product != null ? new ProductDto
                    {
                        Id = product.Id,
                        Label = product.Label,
                        BusinessUnitId = 0
                    }
                    : null,
                Team = team != null ? new TeamDto { Id = team.Id, Label = team.Label } : null,
                Module = module != null ? new ModuleSummaryDto { Id = module.Id, Label = module.Label } : null
                
            };
      
    }
}