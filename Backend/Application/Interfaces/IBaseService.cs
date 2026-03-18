using Domain.DTO;

namespace Application.Interfaces;

public interface IBaseService<T> where T : BaseDto
{
    public Task<List<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(int id);
    public Task<T?> CreateAsync(T entity);
    public Task<T?> UpdateAsync(T entity);
    public Task<bool> DeleteAsync(int id);
}