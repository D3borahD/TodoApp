using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class StepRepository(AppDbContext context) : IBaseRepository<StepDao>, IStepRepository
{
    public Task<List<StepDao>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<StepDao?> GetByIdAsync(int id)
    {
        return await context.Steps
            .Include(step => step.Type)
            .FirstOrDefaultAsync(step => step.Id == id);
    }
   

    public async Task<StepDao> CreateAsync(StepDao stepDao)
    {
        context.Steps.Add(stepDao);
        await context.SaveChangesAsync();
        return stepDao;
    }

    public async Task<StepDao?> UpdateAsync(StepDao stepDao)
    {
        context.Steps.Update(stepDao);
        await context.SaveChangesAsync();
        return stepDao;
    }

    public async Task DeleteAsync(int id)
    {
       var step = await context.Steps.FindAsync(id);
        
        context.Steps.Remove(step);
        context.SaveChangesAsync();
        
    }

    public async Task<List<StepDao>> GetStepByProjectAsync(int projectId)
    => await context.Steps
       // .Include(s => s.Type)
        .Where(s => s.ProjectId == projectId)
        .OrderByDescending(s => s.StartDate)
        .ToListAsync();
}