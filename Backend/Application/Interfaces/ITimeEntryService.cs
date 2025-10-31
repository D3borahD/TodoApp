using Domain.DTO;

namespace Application.Interfaces;

public interface ITimeEntryService 
{
    public Task<List<TimeEntryDto>> GetAllAsync();
    /*public Task<TimeEntryDto?> GetByIdAsync(int id);*/
    public Task<TimeEntryDto> CreateAsync(TimeEntryCreateDto entity);
    /*public Task<TimeEntryDto?> UpdateAsync(TimeEntryDto entity);
    public Task<bool> DeleteAsync(int id);*/
}