using Application.Interfaces;
using BackendApi.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ProductDao>> GetProductsAsync() => await _context.Products.ToListAsync();
    public async Task<ProductDao?> GetProductsByIdAsync(int id) => await _context.Products.FindAsync(id);
    public async Task<ProductDao> CreateProductsAsync(ProductDao productDto)
    {
        _context.Products.Add(productDto);
        await _context.SaveChangesAsync();
        return productDto;
    }

    public async Task<ProductDao> UpdateProductsAsync(ProductDao productDto)
    {
        _context.Products.Update(productDto);
        await _context.SaveChangesAsync();
        return productDto;
    }

    public async Task DeleteProductsAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if(product == null) return;
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}