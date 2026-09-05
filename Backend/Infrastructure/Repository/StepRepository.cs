using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class StepRepository(AppDbContext context) : IStepRepository
{
    public Task<List<StepDao>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<StepDao?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<StepDao> CreateAsync(StepDao stepDao)
    {
        context.Steps.Add(stepDao);
        await context.SaveChangesAsync();
        return stepDao;
    }

    public Task<StepDao?> UpdateAsync(StepDao entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<StepDao>> GetStepByProjectAsync(int projectId)
    => await context.Steps
       // .Include(s => s.Type)
        .Where(s => s.ProjectId == projectId)
        .OrderByDescending(s => s.StartDate)
        .ToListAsync();
}