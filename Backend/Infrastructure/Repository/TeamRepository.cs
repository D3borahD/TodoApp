using Application.Interfaces;
using BackendApi.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _context;
    public TeamRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<TeamDao>> GetTeamsAsync() => await _context.Teams.ToListAsync();
    

    public async Task<TeamDao?> GetTeamByIdAsync(int id) => await _context.Teams.FindAsync(id);
    
    public async Task<TeamDao> CreateTeamAsync(TeamDao team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }
    
    public async Task<TeamDao?> UpdateTeamAsync(TeamDao teamDto)
    {
        _context.Teams.Update(teamDto);
        await _context.SaveChangesAsync();
        return teamDto;
    }

    public async Task DeleteTeamAsync(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team == null) return;
        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();
    }
}

