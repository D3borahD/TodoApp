using BackendApi.Entities;

namespace Application.Interfaces;

public interface IProductRepository
{
    public Task<List<ProductDao>> GetProductsAsync();
    public Task<ProductDao?> GetProductsByIdAsync(int id);
    public Task<ProductDao> CreateProductsAsync(ProductDao productDto);
    public Task<ProductDao> UpdateProductsAsync(ProductDao productDto);
    public Task DeleteProductsAsync(int id);
}