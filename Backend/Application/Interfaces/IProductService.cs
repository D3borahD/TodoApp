using Domain.DTO;

namespace Application.Interfaces;

public interface IProductService
{
    public Task<List<ProductDto>> GetProductsAsync();
    public Task<ProductDto?> GetProductsByIdAsync(int id);
    public Task<ProductDto?> CreateProductAsync(ProductDto ProductDto);
    public Task<ProductDto?> UpdateProductAsync(ProductDto ProductDto);
    public Task<bool> DeleteProductAsync(int id);
}