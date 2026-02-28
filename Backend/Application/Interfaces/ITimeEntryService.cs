using Domain.DTO;

namespace Application.Interfaces;

public interface ITimeEntryService 
{
    public Task<List<TimeEntryDto>> GetAllAsync();
    public Task<IEnumerable<TimeEntryDto>?> GetByCurrentMonthAsync(DateTime date);
    /*public Task<TimeEntryDto> GetByIdAsync(int id);*/
    public Task<TimeEntryDto> CreateAsync(TimeEntryCreateDto entity);
    public Task<TimeEntryDto?> UpdateAsync(TimeEntryCreateDto entity);
    public Task<bool> DeleteAsync(int id);

}