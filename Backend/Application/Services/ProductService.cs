using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
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
            BusinessUnitId = p.BusinessUnitId
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
            BusinessUnitId = productsDao.BusinessUnitId
        };
    }

    public async Task<ProductDto> CreateAsync(ProductDto productDto)
    {
        logger.LogInformation("Creating new product");
        
        ProductDao newProductDao = new ProductDao()
        {
            Label = productDto.Label.ToLower(),
            BusinessUnitId = productDto.BusinessUnitId
        };
        
        var createdProduct = await productRepository.CreateAsync(newProductDao);

        return new ProductDto()
        {
            Id = createdProduct.Id,
            Label = createdProduct.Label,
            BusinessUnitId = createdProduct.BusinessUnitId
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
        productDao.BusinessUnitId = productDto.BusinessUnitId;
        
        var updatedProduct = await productRepository.UpdateAsync(productDao);
        
        return new ProductDto()
        {
            Id = updatedProduct!.Id,
            Label = updatedProduct.Label,
            BusinessUnitId = updatedProduct.BusinessUnitId
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