using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProjectRepository(AppDbContext context) : IBaseRepository<ProjectDao>, IProjectRepository
{
    public async Task<List<ProjectDao>> GetAllAsync() => await context.Projects.ToListAsync();
    public async Task<ProjectDao?> GetByIdAsync(int id) => await context.Projects.FindAsync(id);
   
    /*public async Task<List<ProjectDao>> GetProjectByTypeAsync(int typeId)
    {
        /*var projectList =  await context.Projects
            .Where(p  => p.Type.Id == typeId)
            .Select(p => new ProjectDao()
            {
                Id = p.Id,
                Label = p.Label,
                Type = p.Type,
                Status = p.Status,
            }).ToListAsync();#1#

        return projectList;
    }*/
    
    public async Task<ProjectDao> CreateAsync(ProjectDao pojectDto)
    {
        context.Projects.Add(pojectDto);
        await context.SaveChangesAsync();
        return pojectDto;
    }

    public async Task<ProjectDao?> UpdateAsync(ProjectDao pojectDto)
    {
        context.Projects.Update(pojectDto);
        await context.SaveChangesAsync();
        return pojectDto;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await context.Projects.FindAsync(id);
        if(product == null) return;
        context.Projects.Remove(product);
        await context.SaveChangesAsync();
    }
}