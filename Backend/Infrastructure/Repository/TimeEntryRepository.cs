using Application.Interfaces;
using BackendApi.Entities;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class TimeEntryRepository(AppDbContext context): ITimeEntryRepository
{
    public async Task<List<TimeEntryDao>> GetAllAsync() =>  await context.TimeEntry.ToListAsync();
  
    public async Task<IEnumerable<TimeEntryDao>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await context.TimeEntry
            .Where(t => t.WorkDate >= startDate && t.WorkDate < endDate)
            .ToListAsync();
    }

    public async Task<TimeEntryDao?> GetByIdAsync(int id) => await context.TimeEntry.FirstOrDefaultAsync(t => t.Id == id);
    
    
    public async Task<TimeEntryDao> CreateAsync(TimeEntryDao entity)
    {
        await context.TimeEntry.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TimeEntryDao?> UpdateAsync(TimeEntryDao entity)
    {
        context.TimeEntry.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
    

    public async Task DeleteAsync(int id)
    {
        var timeEntry = await context.TimeEntry.FindAsync(id);
        if (timeEntry == null) return;
        context.TimeEntry.Remove(timeEntry);
        await context.SaveChangesAsync();
    }
}