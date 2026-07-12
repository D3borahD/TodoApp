using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProjectTypeRepository(AppDbContext context) : IBaseRepository<TypeDao>, IProjectTypeRepository
{
    public async Task<List<TypeDao>> GetAllAsync()=> await context.ProjectTypes.ToListAsync();
    public async Task<TypeDao?> GetByIdAsync(int id) => await context.ProjectTypes.FindAsync(id);
    
    public async Task<TypeDao> CreateAsync(TypeDao typeDto)
    {
        context.ProjectTypes.Add(typeDto);
        await context.SaveChangesAsync();
        return typeDto;
    }

    public async Task<TypeDao?> UpdateAsync(TypeDao typeDto)
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