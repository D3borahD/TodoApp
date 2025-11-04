using BackendApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        // Appliquer les migrations si besoin
        await context.Database.MigrateAsync();
     
        if (!context.Teams.Any())
        {
            var teams = new List<TeamDao>
            {
                new TeamDao { Label = "girafe" },
                new TeamDao { Label = "toucan" },
                new TeamDao { Label = "phénix" },
                new TeamDao { Label = "jaguar" },
                new TeamDao { Label = "tiger" },
                new TeamDao { Label = "panther" },
                new TeamDao { Label = "hibou" },
                new TeamDao { Label = "lynx" },
                new TeamDao { Label = "caméléon" }
            };

            context.Teams.AddRange(teams);
            await context.SaveChangesAsync(); 
        }
        
        if (!context.Products.Any())
        {
            var products = new List<ProductDao>
            {
                new ProductDao { Label = "ello hono", BusinessUnitId = 1},
                new ProductDao { Label = "ello adjuster", BusinessUnitId = 1 },
                new ProductDao { Label = "sinaps", BusinessUnitId = 1},
                new ProductDao { Label = "push rec", BusinessUnitId = 1 },
                new ProductDao { Label = "ello auto", BusinessUnitId = 2 },
                new ProductDao { Label = "ello world", BusinessUnitId = 3 }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
        
        if (!context.Modules.Any())
        {
            var modules = new List<ModuleDao>
            {
                new ModuleDao { Label = "socle", ProductId = 1},
                new ModuleDao { Label = "in", ProductId = 1 },
                new ModuleDao { Label = "out", ProductId = 1},
                new ModuleDao { Label = "orchestrateur", ProductId = 1 }
            };

            context.Modules.AddRange(modules);
            await context.SaveChangesAsync();
        }
        
        if (!context.Activity.Any())
        {
            var activities = new List<ActivityDao>
            {
                new ActivityDao { Label = "build" },
                new ActivityDao { Label = "run" },
                new ActivityDao { Label = "ingé quart" }
            };

            context.Activity.AddRange(activities);
            await context.SaveChangesAsync();
        }

    }
    
}