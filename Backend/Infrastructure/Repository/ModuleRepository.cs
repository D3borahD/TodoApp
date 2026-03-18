using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ModuleRepository(AppDbContext context) : IBaseRepository<ModuleDao>, IModuleRepository
{
    public async Task<List<ModuleDao>> GetAllAsync() => await context.Modules.ToListAsync();
    public async Task<ModuleDao?> GetByIdAsync(int id) => await context.Modules.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<ModuleDao> CreateAsync(ModuleDao moduleDao)
    {
         await context.Modules.AddAsync(moduleDao);
         await context.SaveChangesAsync();
         return moduleDao;
    }
    public async Task<List<ModuleDao>> GetModuleByProductAsync(int productId)
    {
        var modules =  await context.Modules
            .Where(m  => m.ProductId == productId)
            .Select(m => new ModuleDao
            {
                Id = m.Id,
                Label = m.Label,
                ProductId = m.ProductId,
                SegmentCode = m.SegmentCode,
                StartDate = m.StartDate,
                Code = m.Code,
            }).ToListAsync();

        return modules;
    }

    public async Task<ModuleDao?> UpdateAsync(ModuleDao moduleDao)
    {
       context.Modules.Update(moduleDao);
       await context.SaveChangesAsync();
       return moduleDao;
    }

    public async Task DeleteAsync(int id)
    {
        var module = await context.Modules.FindAsync(id);
        if (module == null) return;
        context.Modules.Remove(module);
        await context.SaveChangesAsync();
    }
}

