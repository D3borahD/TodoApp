using Application.Interfaces;
using BackendApi.Entities;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProductRepository(AppDbContext context) : IBaseRepository<ProductDao>, IProductRepository
{
    public async Task<List<ProductDao>> GetAllAsync() => await context.Products.ToListAsync();
    public async Task<ProductDao?> GetByIdAsync(int id) => await context.Products.FindAsync(id);
   
    public async Task<List<ProductDao>> GetProductByTeamAsync(int teamId)
    {
        var products =  await context.Products
            .Where(p  => p.TeamId == teamId)
            .Select(p => new ProductDao()
            {
                Id = p.Id,
                Label = p.Label,
                TeamId = p.TeamId,
                Code = p.Code,
            }).ToListAsync();

        return products;
    }
    
    public async Task<ProductDao> CreateAsync(ProductDao productDto)
    {
        context.Products.Add(productDto);
        await context.SaveChangesAsync();
        return productDto;
    }

    public async Task<ProductDao?> UpdateAsync(ProductDao productDto)
    {
        context.Products.Update(productDto);
        await context.SaveChangesAsync();
        return productDto;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if(product == null) return;
        context.Products.Remove(product);
        await context.SaveChangesAsync();
    }
}