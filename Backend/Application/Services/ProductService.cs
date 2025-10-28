using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<List<ProductDto>> GetProductsAsync()
    {
        _logger.LogInformation("Getting all products");
        var productsDao = await _productRepository.GetProductsAsync();
        
        if (!productsDao.Any())
        {
            _logger.LogInformation("No products found");
            return new List<ProductDto>();
        }
        
        var result = productsDao.Select(p => new ProductDto
        {
            Id = p.Id,
            Label = p.Label,
            BusinessUnitId = p.BusinessUnitId
        }).ToList();
        
        _logger.LogInformation("Returning products");
        return result;
    }

    public async Task<ProductDto?> GetProductsByIdAsync(int id)
    {
        _logger.LogInformation("Getting products by id");
        var productsDao = await _productRepository.GetProductsByIdAsync(id);

        if (productsDao == null)
        {
            _logger.LogInformation("No products found");
            return null;
        }

        var result = new ProductDto()
        {
            Id = productsDao.Id,
            Label = productsDao.Label,
            BusinessUnitId = productsDao.BusinessUnitId
        };
        
        _logger.LogInformation("Returning products");
        return result;

    }

    public async Task<ProductDto?> CreateProductAsync(ProductDto ProductDto)
    {
        var productDaoList = await _productRepository.GetProductsAsync();
        int newId = productDaoList.Max(p => p.Id) + 1;

        ProductDao newProductDao = new ProductDao()
        {
            Id = newId,
            Label = ProductDto.Label.ToLower(),
            BusinessUnitId = ProductDto.BusinessUnitId
        };
        
        var createdProduct = await _productRepository.CreateProductsAsync(newProductDao);

        if (createdProduct == null ) return null;
        
        return new ProductDto()
        {
            Id = createdProduct.Id,
            Label = createdProduct.Label,
            BusinessUnitId = createdProduct.BusinessUnitId
        };
    }

    public async Task<ProductDto> UpdateProductAsync(ProductDto ProductDto)
    {
        var productDaoList = await _productRepository.GetProductsAsync();
        var existingProduct = productDaoList.FirstOrDefault(p => p.Id == ProductDto.Id);
        
        if(existingProduct == null) return null;
        
        existingProduct.Label = ProductDto.Label.ToLower();
        
        var updatedProduct = await _productRepository.UpdateProductsAsync(existingProduct);
        
        if (updatedProduct == null) return null;

        return new ProductDto()
        {
            Id = updatedProduct.Id,
            Label = updatedProduct.Label,
            BusinessUnitId = updatedProduct.BusinessUnitId
        };
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var productDaoList = await _productRepository.GetProductsAsync();
        var existingProduct = productDaoList.FirstOrDefault(p => p.Id == id);
        
        if (existingProduct == null) return false;
        
        await _productRepository.DeleteProductsAsync(existingProduct.Id);
        return true;
    }
}