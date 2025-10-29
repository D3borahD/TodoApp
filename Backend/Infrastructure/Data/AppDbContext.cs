using BackendApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<TeamDao> Teams { get; set; }
    public DbSet<ProductDao> Products { get; set; }
    public DbSet<ModuleDao> Modules { get; set; }

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
        
        modelBuilder.Entity<ProductDao>().HasKey(t => t.Id);
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ProductDao>().HasData(
            new ProductDao { Id = 1, Label = "ElloHono", BusinessUnitId = 1},
            new ProductDao { Id = 2, Label = "ElloAdjuster", BusinessUnitId = 1 },
            new ProductDao { Id = 3, Label = "Sinaps", BusinessUnitId = 1},
            new ProductDao { Id = 4, Label = "Push REC", BusinessUnitId = 1 },
            new ProductDao { Id = 5, Label = "ElloAuto", BusinessUnitId = 2 },
            new ProductDao { Id = 6, Label = "ElloWorld", BusinessUnitId = 3 }
        );
        
        modelBuilder.Entity<ModuleDao>().HasKey(t => t.Id);
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ModuleDao>().HasData(
            new ModuleDao { Id = 1, Label = "socle", ProductId = 1},
            new ModuleDao { Id = 2, Label = "in", ProductId = 1 },
            new ModuleDao { Id = 3, Label = "out", ProductId = 1},
            new ModuleDao { Id = 4, Label = "orchestrateur", ProductId = 1 }
        );
    }
}