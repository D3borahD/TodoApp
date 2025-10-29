using Application.Interfaces;
using BackendApi.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ModuleRepository : IModuleRepository
{
    private readonly AppDbContext _context;

    public ModuleRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ModuleDao>> GetModulesAsync()
     => await _context.Modules.ToListAsync();
  
    public async Task<ModuleDao?> GetModuleByIdAsync(int id)
    => await _context.Modules.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<ModuleDao> CreateModuleAsync(ModuleDao moduleDao)
    {
         _context.Modules.AddAsync(moduleDao);
         await _context.SaveChangesAsync();
         return moduleDao;
    }

    public async Task<ModuleDao?> UpdateModuleAsync(ModuleDao moduleDao)
    {
       _context.Modules.Update(moduleDao);
       await _context.SaveChangesAsync();
       return moduleDao;
    }

    public async Task DeleteModuleAsync(int id)
    {
        var module = await _context.Modules.FirstOrDefaultAsync(t => t.Id == id);
        if (module == null) return;
        _context.Modules.Remove(module);
        await _context.SaveChangesAsync();
    }
}

