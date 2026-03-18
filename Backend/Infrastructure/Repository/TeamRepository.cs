using Application.Interfaces;
using BackendApi.Entities;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class TeamRepository(AppDbContext context) : IBaseRepository<TeamDao>
{
    public async Task<List<TeamDao>> GetAllAsync() => await context.Teams.ToListAsync();
    

    public async Task<TeamDao?> GetByIdAsync(int id) => await context.Teams.FindAsync(id);
    
    public async Task<TeamDao> CreateAsync(TeamDao team)
    {
        context.Teams.Add(team);
        await context.SaveChangesAsync();
        return team;
    }
    
    public async Task<TeamDao?> UpdateAsync(TeamDao teamDto)
    {
        context.Teams.Update(teamDto);
        await context.SaveChangesAsync();
        return teamDto;
    }
    
    public async Task DeleteAsync(int id)
    {
        var team = await context.Teams.FindAsync(id);
        if (team == null) return;
        context.Teams.Remove(team);
        await context.SaveChangesAsync();
    }
}

