using System.Globalization;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        // Appliquer les migrations si besoin
        await context.Database.MigrateAsync();
     
        
        /*
        if (!context.Products.Any())
        {
            var products = new List<ProductDao>
            {
                new ProductDao { Label = "AI_HUB", TeamId  = 21, Code = "1"},
                new ProductDao { Label = "AI_ENGINE",TeamId  = 21, Code = "2" },
                new ProductDao { Label = "AUDIO_SCAN",TeamId  = 21, Code = "3"},
              
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }*/
        
   
    }
    
}