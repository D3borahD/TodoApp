using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<TeamDao> Teams { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamDao>().HasKey(t => t.Id);
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TeamDao>().HasData(
            new TeamDao { Id = 1, Label = "Girafe" },
            new TeamDao { Id = 2, Label = "Toucan" },
            new TeamDao { Id = 3, Label = "Phénix" },
            new TeamDao { Id = 4, Label = "Jaguar" },
            new TeamDao { Id = 5, Label = "Tiger" },
            new TeamDao { Id = 6, Label = "Panther" },
            new TeamDao { Id = 7, Label = "Hibou" },
            new TeamDao { Id = 8, Label = "Linx" },
            new TeamDao { Id = 9, Label = "Caméléon" }
        );
    }
}