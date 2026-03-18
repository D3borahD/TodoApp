using BackendApi.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<TeamDao> Teams { get; set; }
    public DbSet<ProductDao> Products { get; set; }
    public DbSet<ModuleDao> Modules { get; set; }
    public DbSet<ActivityDao> Activity { get; set; }
    
    public DbSet<TimeEntryDao> TimeEntry { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TeamDao>()
            .HasKey(t => t.Id);
        
        modelBuilder.Entity<TeamDao>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd(); 

        modelBuilder.Entity<ProductDao>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<ProductDao>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd(); 

        modelBuilder.Entity<ModuleDao>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<ModuleDao>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd(); 
        
        modelBuilder.Entity<ActivityDao>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<ActivityDao>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd(); 
        
        modelBuilder.Entity<TimeEntryDao>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<TimeEntryDao>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd(); 
    }
}