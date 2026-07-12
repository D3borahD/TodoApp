using Domain.Entities;

namespace Application.Interfaces;

public interface IBaseService<T> where T : IEntity
{
    public Task<List<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(int id);
    public Task<T?> CreateAsync(T entity);
    public Task<T?> UpdateAsync(T entity);
    public Task<bool> DeleteAsync(int id);
}