using Application.Interfaces;
using BackendApi.Entities;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ActivityRepository(AppDbContext context) : IBaseRepository<ActivityDao>
{
    public async Task<List<ActivityDao>> GetAllAsync() => await context.Activity.ToListAsync();

    public async Task<ActivityDao?> GetByIdAsync(int id) => await context.Activity.FirstOrDefaultAsync(t => t.Id == id);
  

    public async Task<ActivityDao> CreateAsync(ActivityDao entity)
    {
        await context.Activity.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<ActivityDao?> UpdateAsync(ActivityDao entity)
    {
        context.Activity.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var activity = await context.Activity.FindAsync(id);
        if (activity == null) return;
        context.Activity.Remove(activity);
        await context.SaveChangesAsync();
    }
}