using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProjectRepository(AppDbContext context) : IBaseRepository<ProjectDao>, IProjectRepository
{
    public async Task<List<ProjectDao>> GetAllAsync()
    {
        return await context.Projects
            .Include(project => project.StepList)
            .OrderByDescending(project => project.Status)
            .ToListAsync();
    }
    
    public async Task<List<ProjectDao>> SearchAsync(ProjectQueryDto? projectQueryDto = null)
    {
        IQueryable<ProjectDao>  query =  context.Projects
            .Include(project => project.StepList)
            .AsNoTracking();
        
        if(projectQueryDto != null)
        {
            GetProjectQuery(projectQueryDto, query);
        };
        
        return await query
            .OrderBy(project => project.Status)
            .ToListAsync();
    }
    
    public async Task<ProjectDao?> GetByIdAsync(int id)
    { 
        return await context.Projects
        .Include(project => project.StepList)
        .Where(project => project.Id == id)
        .FirstOrDefaultAsync();
    }
    
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
    
    private IQueryable<ProjectDao> GetProjectQuery(ProjectQueryDto projectQueryDto, IQueryable<ProjectDao> query)
    {
        if (projectQueryDto.Status != null)
        {
            query = query.Where(project => project.Status == projectQueryDto.Status);
        }

        if (!string.IsNullOrWhiteSpace(projectQueryDto.Label))
        {
            query = query.Where(project => project.Label.Contains(projectQueryDto.Label));
        }

        if (projectQueryDto.ProjectTypeId != null)
        {
           // query = query.Where(project => project.StepList.Any(step => step.Type.Id == projectQueryDto.ProjectTypeId));
        }
        
        return query;
    }
}