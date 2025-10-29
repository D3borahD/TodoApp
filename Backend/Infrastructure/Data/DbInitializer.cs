using BackendApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        // Appliquer les migrations si besoin
        await context.Database.MigrateAsync();
        
        {
            var teams = new List<TeamDao>
            {
                new TeamDao { Id = 1, Label = "girafe" },
                new TeamDao { Id = 2, Label = "toucan" },
                new TeamDao { Id = 3, Label = "phénix" },
                new TeamDao { Id = 4, Label = "jaguar" },
                new TeamDao { Id = 5, Label = "tiger" },
                new TeamDao { Id = 6, Label = "panther" },
                new TeamDao { Id = 7, Label = "hibou" },
                new TeamDao { Id = 8, Label = "linx" },
                new TeamDao { Id = 9, Label = "caméléon" }
            };

            context.Teams.AddRange(teams);
            await context.SaveChangesAsync();
        }
        
        if (!context.Products.Any())
        {
            var products = new List<ProductDao>
            {
                new ProductDao { Id = 1, Label = "ello hono", BusinessUnitId = 1},
                new ProductDao { Id = 2, Label = "ello adjuster", BusinessUnitId = 1 },
                new ProductDao { Id = 3, Label = "sinaps", BusinessUnitId = 1},
                new ProductDao { Id = 4, Label = "push rec", BusinessUnitId = 1 },
                new ProductDao { Id = 5, Label = "ello auto", BusinessUnitId = 2 },
                new ProductDao { Id = 6, Label = "ello world", BusinessUnitId = 3 }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
        
        if (!context.Modules.Any())
        {
            var modules = new List<ModuleDao>
            {
                new ModuleDao { Id = 1, Label = "socle", ProductId = 1},
                new ModuleDao { Id = 2, Label = "in", ProductId = 1 },
                new ModuleDao { Id = 3, Label = "out", ProductId = 1},
                new ModuleDao { Id = 4, Label = "orchestrateur", ProductId = 1 }
            };

            context.Modules.AddRange(modules);
            await context.SaveChangesAsync();
        }
        
        if (!context.Activity.Any())
        {
            var activities = new List<ActivityDao>
            {
                new ActivityDao { Id = 1, Label = "build" },
                new ActivityDao { Id = 2, Label = "run" },
                new ActivityDao { Id = 3, Label = "ingé quart" }
            };

            context.Activity.AddRange(activities);
            await context.SaveChangesAsync();
        }

    }
    
}