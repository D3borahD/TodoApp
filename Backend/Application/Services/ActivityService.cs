using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ActivityService(IBaseRepository<ActivityDao> activityRepository, ILogger<ActivityService> logger):IBaseService<ActivityDto>
{
    public async Task<List<ActivityDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all activities.");
        var activities = await activityRepository.GetAllAsync();

        if (!activities.Any())
        {
            logger.LogInformation("No activities found.");
            return new List<ActivityDto>();
        }
        
        return activities.Select(m => new ActivityDto()
        {
            Id = m.Id,
            Label =  m.Label
        }).ToList();
    }

    public async Task<ActivityDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting activities by id.");
        var activity = await activityRepository.GetByIdAsync(id);

        if (activity == null) 
        {
            logger.LogInformation("No activity found.");
            return null;
        }
        
        return new ActivityDto()
        {
            Id = activity.Id,
            Label = activity.Label
        };
    }

    public async Task<ActivityDto?> CreateAsync(ActivityDto entity)
    {
        logger.LogInformation("Creating new activity.");
        if (entity.Code == null) return null;
        
        ActivityDao activity = new ActivityDao()
        { Label = entity.Label.ToLower(), 
            Code = entity.Code
        };
        
        var createdModule = await activityRepository.CreateAsync(activity);
            
        return new ActivityDto()
        { Id = createdModule.Id,
            Label = createdModule.Label,
            Code = createdModule.Code
        };
    }

    public async Task<ActivityDto?> UpdateAsync(ActivityDto entity)
    {
        var activity = await activityRepository.GetByIdAsync(entity.Id);
        if (activity == null)
        {
            logger.LogInformation("Activity not found.");
            return null;
        }

        activity.Label = entity.Label.ToLower();
        
        var updatedModule = await activityRepository.UpdateAsync(activity);
        if (updatedModule == null) return null;

        return new ActivityDto()
        {
            Id = updatedModule.Id,
            Label = updatedModule.Label
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var activity = await activityRepository.GetByIdAsync(id);

        if (activity == null)
        {
            logger.LogWarning("Activity with id {Id} not found for deletion.", id);            
            return false;
        }
        
        await activityRepository.DeleteAsync(activity.Id);
        return true;
    }
}