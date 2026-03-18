using BackendApi.Entities;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITimeEntryRepository
{
    public Task<List<TimeEntryDao>> GetAllAsync();
    public Task<TimeEntryDao?> GetByIdAsync(int id);
    public Task<IEnumerable<TimeEntryDao>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    public Task<TimeEntryDao> CreateAsync(TimeEntryDao entity);
    public Task<TimeEntryDao?> UpdateAsync(TimeEntryDao entity);
    public Task DeleteAsync(int id);
}