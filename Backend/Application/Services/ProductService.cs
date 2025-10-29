using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
    : IProductService
{
    public async Task<List<ProductDto>> GetProductsAsync()
    {
        logger.LogInformation("Getting all products");
        var productsDao = await productRepository.GetProductsAsync();
        
        if (!productsDao.Any())
        {
            logger.LogInformation("No products found");
            return new List<ProductDto>();
        }
        
        var result = productsDao.Select(p => new ProductDto
        {
            Id = p.Id,
            Label = p.Label,
            BusinessUnitId = p.BusinessUnitId
        }).ToList();
        
        logger.LogInformation("Returning products");
        return result;
    }

    public async Task<ProductDto?> GetProductsByIdAsync(int id)
    {
        logger.LogInformation("Getting products by id");
        var productsDao = await productRepository.GetProductsByIdAsync(id);

        if (productsDao == null)
        {
            logger.LogInformation("No products found");
            return null;
        }

        var result = new ProductDto()
        {
            Id = productsDao.Id,
            Label = productsDao.Label,
            BusinessUnitId = productsDao.BusinessUnitId
        };
        
        logger.LogInformation("Returning products");
        return result;

    }

    public async Task<ProductDto?> CreateProductAsync(ProductDto productDto)
    {
        var productDaoList = await productRepository.GetProductsAsync();
        int newId = productDaoList.Max(p => p.Id) + 1;

        ProductDao newProductDao = new ProductDao()
        {
            Id = newId,
            Label = productDto.Label.ToLower(),
            BusinessUnitId = productDto.BusinessUnitId
        };
        
        var createdProduct = await productRepository.CreateProductsAsync(newProductDao);

        return new ProductDto()
        {
            Id = createdProduct.Id,
            Label = createdProduct.Label,
            BusinessUnitId = createdProduct.BusinessUnitId
        };
    }

    public async Task<ProductDto?> UpdateProductAsync(ProductDto productDto)
    {
        var productDaoList = await productRepository.GetProductsAsync();
        var existingProduct = productDaoList.FirstOrDefault(p => p.Id == productDto.Id);
        
        if(existingProduct == null) return null;
        
        existingProduct.Label = productDto.Label.ToLower();
        
        var updatedProduct = await productRepository.UpdateProductsAsync(existingProduct);
        
        return new ProductDto()
        {
            Id = updatedProduct.Id,
            Label = updatedProduct.Label,
            BusinessUnitId = updatedProduct.BusinessUnitId
        };
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var productDaoList = await productRepository.GetProductsAsync();
        var existingProduct = productDaoList.FirstOrDefault(p => p.Id == id);
        
        if (existingProduct == null) return false;
        
        await productRepository.DeleteProductsAsync(existingProduct.Id);
        return true;
    }
}