using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductService(ILogger<ProductService> logger, IBaseRepository<ProductDao> productRepository, 
    IModuleRepository moduleRepository
)
    : IBaseService<ProductDto>, IProductService
{
    public async Task<List<ProductDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all products");
        var products = await productRepository.GetAllAsync();
        
        if (!products.Any())
        {
            logger.LogInformation("No products found");
            return new List<ProductDto>();
        }
        
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Label = p.Label,
        }).ToList();
    }

    public async Task<List<ModuleDto>> GetModuleByProductAsync(int productId)
    {
        var modules = await moduleRepository.GetModuleByProductAsync(productId);
        
        return modules.Select(m => new ModuleDto
        {
            Id = m.Id,
            Label = m.Label,
        }).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting products by id");
        var productsDao = await productRepository.GetByIdAsync(id);

        if (productsDao == null)
        {
            logger.LogInformation("No products found");
            return null;
        }

        return new ProductDto()
        {
            Id = productsDao.Id,
            Label = productsDao.Label,
        };
    }

    public async Task<ProductDto?> CreateAsync(ProductDto productDto)
    {
        logger.LogInformation("Creating new product");
        if (productDto.Code == null) return null;
        
        ProductDao newProductDao = new ProductDao()
        {
            Label = productDto.Label.ToLower(),
            Code = productDto.Code
        };
        
        var createdProduct = await productRepository.CreateAsync(newProductDao);

        return new ProductDto()
        {
            Id = createdProduct.Id,
            Label = createdProduct.Label,

        };
    }

    public async Task<ProductDto?> UpdateAsync(ProductDto productDto)
    {
        var productDao = await productRepository.GetByIdAsync(productDto.Id);
        if(productDao == null)
        {
            logger.LogInformation("No products found");
            return null;
        }
        
        productDao.Label = productDto.Label.ToLower();
        
        var updatedProduct = await productRepository.UpdateAsync(productDao);
        
        return new ProductDto()
        {
            Id = updatedProduct!.Id,
            Label = updatedProduct.Label
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var productDao = await productRepository.GetByIdAsync(id);
        
        if (productDao == null)
        {
            logger.LogInformation("No products found");
            return false;
        }
        
        await productRepository.DeleteAsync(productDao.Id);
        return true;
    }
}