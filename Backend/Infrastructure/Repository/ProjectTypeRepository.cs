using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProjectTypeRepository(AppDbContext context) : IBaseRepository<ProjectTypeDao>, IProjectTypeRepository
{
    public async Task<List<ProjectTypeDao>> GetAllAsync()=> await context.ProjectTypes.ToListAsync();
    public async Task<ProjectTypeDao?> GetByIdAsync(int id) => await context.ProjectTypes.FindAsync(id);
    
    public async Task<ProjectTypeDao> CreateAsync(ProjectTypeDao typeDto)
    {
        context.ProjectTypes.Add(typeDto);
        await context.SaveChangesAsync();
        return typeDto;
    }

    public async Task<ProjectTypeDao?> UpdateAsync(ProjectTypeDao typeDto)
    {
        context.ProjectTypes.Update(typeDto);
        await context.SaveChangesAsync();
        return typeDto;
    }

    public async Task DeleteAsync(int id)
    {
        var type = await context.ProjectTypes.FindAsync(id);
        if(type == null) return;
        context.ProjectTypes.Remove(type);
        await context.SaveChangesAsync();
    }
    
}