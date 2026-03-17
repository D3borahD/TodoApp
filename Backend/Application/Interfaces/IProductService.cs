using Domain.DTO;

namespace Application.Interfaces;

public interface IProductService : IBaseService<ProductDto>
{
    public Task<List<ModuleDto>> GetModuleByProductAsync(int productId);
}